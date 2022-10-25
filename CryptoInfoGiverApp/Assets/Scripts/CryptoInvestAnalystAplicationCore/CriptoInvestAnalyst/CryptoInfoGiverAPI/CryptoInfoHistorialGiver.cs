using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CryptoInfoGiverSpace
{
    [Serializable]
    public class CryptoInfoHistorialGiver : ICryptoInfoHistorialGiver
    {
        private const string _daysPriceAskAddres = "v2/histoday";
        private const string _hourPriceAskAddres = "v2/histohour";
        private const string _minutePriceAskAddres = "v2/histominute";

        private const string _getCurrentDayPriceAskAddres = "pricehistorical";

        private const int _cryptoCurseInfoStringLength = 11;

        const int _priceAnswerDayInfoRowUpOffset = 15;
        const int _priceAnswerDayInfoRowDownOffset = 3;

        const int _dataPriceRowIndex = 3;

        [SerializeField] private CryptoInfoGiver _cryptoInfoGiver;

        [SerializeField] private CryptoParser _cryptoParser;

        public CryptoInfoHistorialGiver(CryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
            _cryptoParser = new CryptoParser(_cryptoInfoGiver);
        }

        public IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length)
        {
            try
            {
                return GetAskAddresPrice(_daysPriceAskAddres, comperable, comperer, length);
            }
            catch (Exception exeption)
            {
                throw new CryptoBaseAskExteption(exeption);
            }
        }

        public IEnumerable<CryptoCurseInfo> GetHourPrice(Crypto comperable, Crypto comperer, uint length)
        {
            try
            {
                return GetAskAddresPrice(_hourPriceAskAddres, comperable, comperer, length);
            }
            catch (Exception exeption)
            {
                throw new CryptoBaseAskExteption(exeption);
            }
        }

        public IEnumerable<CryptoCurseInfo> GetMinutePrice(Crypto comperable, Crypto comperer, uint length)
        {
            try
            {
                return GetAskAddresPrice(_minutePriceAskAddres, comperable, comperer, length);
            }
            catch (Exception exeption)
            {
                throw new CryptoBaseAskExteption(exeption);
            }
        }

        public double GetDatePrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            try
            {
                if (_cryptoInfoGiver.ContainsCryptoApiKey(comperable) == false || _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false)
                    throw new ArgumentException();

                var parsedAnswer = _cryptoInfoGiver.Parse(_getCurrentDayPriceAskAddres, GetExtraDayPriceSourceArguments(comperable, comperer, date));

                return _cryptoParser.ReadPrice(parsedAnswer[_dataPriceRowIndex], comperable);
            }
            catch (Exception exeption)
            {
                throw new CryptoBaseAskExteption(exeption);
            }
        }

        private IEnumerable<CryptoCurseInfo> GetAskAddresPrice(string addres, Crypto comperable, Crypto comperer, uint length)
        {
            if (_cryptoInfoGiver.ContainsCryptoApiKey(comperable) == false || _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false)
                throw new ArgumentException();

            var parsedAnswer = _cryptoInfoGiver.Parse(addres, GetExtraTimeSpanPriceSourceArguments(comperable, comperer, length));

            for (int i = _priceAnswerDayInfoRowUpOffset; i < parsedAnswer.Count - _priceAnswerDayInfoRowDownOffset; i += _cryptoCurseInfoStringLength)
            {
                yield return ParseInfo(parsedAnswer, i);
            }
        }

        private string GetExtraDayPriceSourceArguments(Crypto comperable, Crypto comperer, DateTimeOffset dateTime)
        {
            return $"?fsym={_cryptoInfoGiver.CryptoToApiName(comperer)}&tsyms={_cryptoInfoGiver.CryptoToApiName(comperable)}&ts={dateTime.ToUnixTimeSeconds()}";
        }

        private string GetExtraTimeSpanPriceSourceArguments(Crypto comperable, Crypto comperer, uint length)
        {
            return $"?fsym={_cryptoInfoGiver.CryptoToApiName(comperer)}&tsym={_cryptoInfoGiver.CryptoToApiName(comperable)}&&limit={length}";
        }

        private CryptoCurseInfo ParseInfo(List<string> parsedAnswer, int startIndex)
        {
            const int startBreaksOffset = 1;
            const int endBreaksOffset = 2;

            try
            {
                if (IndexOutOfBounds(parsedAnswer, startIndex) || IndexOutOfBounds(parsedAnswer, startIndex + _cryptoCurseInfoStringLength))
                    throw new ArgumentException();

                var cryptoCurseInfoString = new StringBuilder();

                for (int i = startIndex; i < startIndex + _cryptoCurseInfoStringLength; i++)
                {
                    cryptoCurseInfoString.Append(parsedAnswer[i]);

                    if (i - startIndex >= startBreaksOffset && i - startIndex < _cryptoCurseInfoStringLength - endBreaksOffset)
                        cryptoCurseInfoString.Append(CryptoInfoGiver.ApiDivideChar);
                }

                return JsonConvert.DeserializeObject<CryptoCurseInfo>(cryptoCurseInfoString.ToString());
            }
            catch (Exception exceotion)
            {
                throw new CryptoBaseAskExteption(exceotion);
            }
        }

        private bool IndexOutOfBounds(List<string> parsedAnswer, int index)
        {
            return index < 0 || index >= parsedAnswer.Count;
        }
    }
}
