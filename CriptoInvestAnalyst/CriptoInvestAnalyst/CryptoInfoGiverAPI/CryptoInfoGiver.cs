using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;
using Extenstions;
using System.Linq;

namespace CryptoInfoGiverSpace
{
    public class CryptoInfoGiver : ICryptoInfoGiver
    {
        private const string _apyKey = "505986c3d636b5f282102aab2d210bb525e95814b57d50bce32a5a05de7c48bf";

        private const string _url = "https://min-api.cryptocompare.com/data/";

        private static readonly IReadOnlyDictionary<Crypto, string> _cryptoToApiName = new Dictionary<Crypto, string>()
        {
            {Crypto.BTC, "BTC" },
            {Crypto.USD, "USD" },
            {Crypto.ETH, "ETH" }
        };

        private static readonly Parser _parser = new Parser();

        public const char ApiDivideChar = ',';
        public const char FloatDotChar = ',';
        public const char ApiFloatDotChar = '.';

        private readonly CryptoParser _cryptoParser;

        public CryptoInfoGiver()
        {
            _cryptoParser = new CryptoParser((ICryptoInfoGiver)(this));
        }

        #region BlockChainData
        private const string _getAvaibleCoinsAskAddres = "blockchain/list";

        public IEnumerable<CryptoLableInfo> GetAvailableCoins()
        {
            var parsedAnswer = Parse(_getAvaibleCoinsAskAddres);

            const int elementInfoLength = 7;
            const int availableArrayStart = 10;

            const int elementInfoOffsetLeft = 2;
            const int elementInfoOffsetRight = 1;

            var cryptoFullInfos = new List<CryptoLableInfo>();

            for (int i = availableArrayStart; i < parsedAnswer.Count - (parsedAnswer.Count % elementInfoLength); i += elementInfoLength)
            {
                var elementInfo = new StringBuilder();

                elementInfo.Append("{");

                for (int j = elementInfoOffsetLeft; j < elementInfoLength - elementInfoOffsetRight; j++)
                {
                    elementInfo.Append(parsedAnswer[i + j] + ",");
                }

                elementInfo.Append("}");

                var newCryptoFullInfo = JsonConvert.DeserializeObject<CryptoLableInfo>(elementInfo.ToString());

                cryptoFullInfos.Add(newCryptoFullInfo);
            }

            return cryptoFullInfos;
        }
        #endregion BlockChainData

        public string GetCryptoCollectionApiCode(IEnumerable<Crypto> cryptos)
        {
            try
            {
                var apiCode = new StringBuilder();

                foreach (var cripto in cryptos)
                {
                    apiCode.Append(_cryptoToApiName[cripto] + ApiDivideChar);
                }

                return apiCode.ToString().Remove(apiCode.Length - 1);
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }

        public List<string> Parse(string addres, string extraArguments = "")
        {
            try
            { 
            var answer = _parser.GetRequest((_url + addres + extraArguments), new Dictionary<string, string>());
            return _cryptoParser.Parse(answer);
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }

        public bool ContainsCryptoApiKey(Crypto crypto)
        {
            return _cryptoToApiName.ContainsKey(crypto);
        }

        public string CryptoToApiName(Crypto crypto)
        {
            try
            {
                return _cryptoToApiName[crypto];
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }

        protected static string GetAPIKey()
        {
            return $"?api_key={_apyKey}";
        }
    }
}
