using Extenstions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoInfoGiverSpace
{
    [Serializable]
    public class CryptoInfoPriceGiver : ICryptoInfoPriceGiver
    {
        private const string _getPriceAskAddres = "price";
        private const string _getPricemultiAskAddres = "pricemulti";

        private const int _compareResultOffsetRowIndex = 1;

        private const int _parsemultiCompareResultOffsetRowIndex = 3;

        private readonly ICryptoInfoGiver _cryptoInfoGiver;
        private readonly CryptoParser _cryptoParser;

        public CryptoInfoPriceGiver(ICryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
            _cryptoParser = new CryptoParser(_cryptoInfoGiver);
        }

        public double GetPrice(Crypto comperable, Crypto comperer)
        {
            try
            {
                if (_cryptoInfoGiver.ContainsCryptoApiKey(comperable) == false || _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false)
                    throw new ArgumentException();

                var addresAppend = GetExtraSourceArguments(new[] { comperable }, comperer);
                var parsedAnswer = _cryptoInfoGiver.Parse(_getPriceAskAddres, addresAppend);

                return _cryptoParser.ReadPrice(parsedAnswer[_compareResultOffsetRowIndex], comperable);
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }

        public IEnumerable<double> GetPrices(Crypto[] comperables, Crypto comperer)
        {
            if (comperables.Any(comparable => _cryptoInfoGiver.ContainsCryptoApiKey(comparable) == false)
                || _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false)
                throw new CryptoBaseAskExteption(new ArgumentException());

            var addresAppend = GetExtraSourceArguments(comperables, comperer);
            var parsedAnswer = _cryptoInfoGiver.Parse(_getPriceAskAddres, addresAppend);

            for (int currentRow = _compareResultOffsetRowIndex; currentRow < parsedAnswer.Count - _compareResultOffsetRowIndex; currentRow++)
            {
                yield return _cryptoParser.ReadPrice(parsedAnswer[currentRow], comperables[currentRow - _compareResultOffsetRowIndex]);
            }
        }

        public Dictionary<Crypto, List<PriceInfo>> GetPrices(Crypto[] comperables, Crypto[] comperers)
        {
            try
            {
                if (comperables.Any(comparable => _cryptoInfoGiver.ContainsCryptoApiKey(comparable) == false) ||
                    comperers.Any(comperer => _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false))
                    throw new ArgumentException();

                var addresAppend = GetExtraSourceArguments(comperables, comperers);
                var parsedAnswer = _cryptoInfoGiver.Parse(_getPricemultiAskAddres, addresAppend);

                return ReadPrices(parsedAnswer, comperables, comperers);
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }

        private Dictionary<Crypto, List<PriceInfo>> ReadPrices(List<string> parsedAnswer, Crypto[] comperables, Crypto[] comperers)
        {
            try
            {
                int currentParsedAnswerIndex = _parsemultiCompareResultOffsetRowIndex;

                var comperableToComperersPrice = new Dictionary<Crypto, List<PriceInfo>>();

                for (int currentComperer = 0; currentComperer < comperers.Length; currentComperer++)
                {
                    comperableToComperersPrice[comperers[currentComperer]] = new List<PriceInfo>();

                    for (int currentComperable = 0; currentComperable < comperables.Length; currentComperable++)
                    {
                        var newPriceInfo = new PriceInfo(comperables[currentComperable], _cryptoParser.ReadPrice(parsedAnswer[currentParsedAnswerIndex], comperables[currentComperable]));
                        comperableToComperersPrice[comperers[currentComperer]].Add(newPriceInfo);

                        currentParsedAnswerIndex++;
                    }

                    currentParsedAnswerIndex += 3;
                }

                return comperableToComperersPrice;
            }
            catch (Exception exception)
            {
                throw new CryptoBaseAskExteption(exception);
            }
        }

        private string GetExtraSourceArguments(Crypto[] comperables, Crypto comperer)
        {
            return $"?fsym={_cryptoInfoGiver.CryptoToApiName(comperer)}&tsyms={_cryptoInfoGiver.GetCryptoCollectionApiCode(comperables)}";
        }

        private string GetExtraSourceArguments(Crypto comperable, Crypto[] comperers)
        {
            return $"?fsyms={_cryptoInfoGiver.GetCryptoCollectionApiCode(comperers)}&tsym={_cryptoInfoGiver.CryptoToApiName(comperable)}";
        }

        private string GetExtraSourceArguments(Crypto[] comperables, Crypto[] comperers)
        {
            return $"?fsyms={_cryptoInfoGiver.GetCryptoCollectionApiCode(comperers)}&tsyms={_cryptoInfoGiver.GetCryptoCollectionApiCode(comperables)}";
        }
    }
}
