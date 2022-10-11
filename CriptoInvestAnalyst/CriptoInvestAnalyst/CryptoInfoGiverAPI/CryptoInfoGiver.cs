using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;
using Extenstions;
using System.Linq;

namespace CryptoInvestAnalyst
{
    public class CryptoInfoGiver : ICryptoInfoGiver
    {
        private const string _apyKey = "505986c3d636b5f282102aab2d210bb525e95814b57d50bce32a5a05de7c48bf";

        #region Config
        private static readonly IReadOnlyDictionary<Crypto, string> _cryptoToApiName = new Dictionary<Crypto, string>()
        {
            {Crypto.BTC, "BTC" },
            {Crypto.USD, "USD" },
            {Crypto.ETH, "ETH" }
        };

        private static readonly Parser _parser = new Parser();
        private static readonly CryptoParser _cryptoParser = new CryptoParser();

        private const char _apiDivideChar = ',';
        private const char _floatDotChar = ',';
        private const char _apiFloatDotChar = '.';

        #endregion Config

        #region Price
        public class PriceGiver : ICriptoInfoPriceGiver
        {
            private const string _getPriceAskAddres = "https://min-api.cryptocompare.com/data/price";

            private const int _compareResultOffsetRowIndex = 1;

            private const int _compareResultOffsetX = 4;

            public double GetPrice(Crypto comperable, Crypto comperer)
            {
                if (ContainsCryptoApiKey(comperable) == false || _cryptoToApiName.ContainsKey(comperer) == false)
                    throw new ArgumentException();

                var getPriceParameters = new Dictionary<string, string>()
                {
                    {"fsym", _cryptoToApiName[comperer]},
                    {"tsyms", _cryptoToApiName[comperable]}
                };

                var parsedAnswer = Parse(_getPriceAskAddres, getPriceParameters);

                foreach (var h in parsedAnswer)
                    Console.WriteLine(h);

                return GetPrice(parsedAnswer[_compareResultOffsetRowIndex], comperable);
            }

            public IEnumerable<double> GetPrices(Crypto[] comperables, Crypto comperer)
            {
                var _getPriceParameters = new Dictionary<string, string>()
                {
                };

                if (comperables.Any(comparable => ContainsCryptoApiKey(comparable) == false)
                    || _cryptoToApiName.ContainsKey(comperer) == false)
                    throw new ArgumentException();

                var addresAppend = $"?fsym={_cryptoToApiName[comperer]}&tsyms={GetCryptoCollectionApiCode(comperables)}";
                var parsedAnswer = Parse(_getPriceAskAddres, _getPriceParameters);

                for (int currentRow = _compareResultOffsetRowIndex; currentRow < parsedAnswer.Count - _compareResultOffsetRowIndex; currentRow++)
                {
                    yield return GetPrice(parsedAnswer[currentRow], comperables[currentRow - _compareResultOffsetRowIndex]);
                }
            }

            private double GetPrice(string parsedAnswerPriceInfo, Crypto comperable)
            {
                StringBuilder compareResult = new StringBuilder();

                for (int i = _compareResultOffsetX + _cryptoToApiName[comperable].Length - 1; i < parsedAnswerPriceInfo.Length; i++)
                {
                    if (parsedAnswerPriceInfo[i].IsMatch() == false)
                        break;

                    if (parsedAnswerPriceInfo[i] == _apiFloatDotChar)
                        compareResult.Append(_floatDotChar);
                    else
                        compareResult.Append(parsedAnswerPriceInfo[i]);
                }

                return double.Parse(compareResult.ToString());
            }
        }
        #endregion Price

        #region BlockChainData
        private const string _getAvaibleCoinsAskAddres = "https://min-api.cryptocompare.com/data/blockchain/list";

        public IEnumerable<CriptoFullInfo> GetAvailableCoins()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
            };

            var parsedAnswer = Parse(GetAddresWitchAPIKey(_getAvaibleCoinsAskAddres), parameters);

            const int elementInfoLength = 7;
            const int availableArrayStart = 10;

            const int elementInfoOffsetLeft = 2;
            const int elementInfoOffsetRight = 1;

            var cryptoFullInfos = new List<CriptoFullInfo>();

            for (int i = availableArrayStart; i < parsedAnswer.Count - (parsedAnswer.Count % elementInfoLength); i += elementInfoLength)
            {
                var elementInfo = new StringBuilder();

                elementInfo.Append("{");

                for (int j = elementInfoOffsetLeft; j < elementInfoLength - elementInfoOffsetRight; j++)
                {
                    elementInfo.Append(parsedAnswer[i + j] + ",");
                }

                elementInfo.Append("}");

                var newCryptoFullInfo = JsonConvert.DeserializeObject<CriptoFullInfo>(elementInfo.ToString());

                cryptoFullInfos.Add(newCryptoFullInfo);
            }

            return cryptoFullInfos;
        }
        #endregion BlockChainData

        #region Helping
        protected static string GetCryptoCollectionApiCode(IEnumerable<Crypto> cryptos)
        {
            var apiCode = new StringBuilder();

            foreach (var cripto in cryptos)
            {
                apiCode.Append(_cryptoToApiName[cripto] + _apiDivideChar);
            }

            return apiCode.ToString().Remove(apiCode.Length - 1);
        }

        protected static List<string> Parse(string addres, Dictionary<string, string> parameters, string extraArguments = "")
        {
            var answer = _parser.GetRequest((addres + extraArguments), parameters);
            return _cryptoParser.Parse(answer);
        }

        protected static string GetAddresWitchAPIKey(string addres)
        {
            return addres + $"?api_key={_apyKey}";
        }

        protected static bool ContainsCryptoApiKey(Crypto crypto)
        {
            return _cryptoToApiName.ContainsKey(crypto);
        }

        #endregion Helping
    }
}
