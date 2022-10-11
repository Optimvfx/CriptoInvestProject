using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;

namespace CryptoInvestAnalyst
{
    public class CryptoInfoGiver : ICryptoInfoGiver
    {
        private readonly string _apyKey = "505986c3d636b5f282102aab2d210bb525e95814b57d50bce32a5a05de7c48bf";

        private readonly IReadOnlyDictionary<Crypto, string> _cryptoToAPIName = new Dictionary<Crypto, string>()
        {
            {Crypto.BTC, "BTC" },
            {Crypto.USD, "USD" }
        };

        private readonly string _getAvaibleCoinsAskAddres = "https://min-api.cryptocompare.com/data/blockchain/list";
        private readonly string _getPriceAskAddres = "https://min-api.cryptocompare.com/data/price";

        private readonly Parser _parser = new Parser();
        private readonly CryptoParser _cryptoParser = new CryptoParser();

        public double GetPrice(Crypto comperable, Crypto comperer)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
            };

            if (_cryptoToAPIName.ContainsKey(comperable) == false || _cryptoToAPIName.ContainsKey(comperer) == false)
                throw new ArgumentException();

            var addresAppend = $"?fsym={_cryptoToAPIName[comperer]}&tsyms={_cryptoToAPIName[comperable]}";
            var parsedAnswer = Parse(_getPriceAskAddres + addresAppend, parameters);

            const int compareResultIndex = 1;
            const int compareResultXPosition = 4;

            const char apiFloatDotChar = '.';
            const char floatDotChar = ',';

            StringBuilder compareResult = new StringBuilder();

            for (int i = compareResultXPosition + _cryptoToAPIName[comperable].Length - 1; i < parsedAnswer[compareResultIndex].Length; i++)
            {
                if (char.IsNumber(parsedAnswer[compareResultIndex][i]) == false && parsedAnswer[compareResultIndex][i] != apiFloatDotChar)
                    break;

                Console.WriteLine(parsedAnswer[compareResultIndex].ToString());

                if (parsedAnswer[compareResultIndex][i] == apiFloatDotChar)
                    compareResult.Append(floatDotChar);
                else
                    compareResult.Append(parsedAnswer[compareResultIndex][i]);
            }

            return double.Parse(compareResult.ToString());
        }

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

        #region Helping
        private List<string> Parse(string addres, Dictionary<string, string> parameters)
        {
            var answer = _parser.GetRequest(addres, parameters);
            return  _cryptoParser.Parse(answer);
        }

        private string GetAddresWitchAPIKey(string addres)
        {
            return addres + $"?api_key={_apyKey}";
        }

        #endregion Helping
    }
}
