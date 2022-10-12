using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoInvestAnalyst
{
    public class CryptoInfoHistorialGiver : ICryptoInfoHistorialGiver
    {
        private readonly ICryptoInfoGiver _cryptoInfoGiver;

        private const string _daysPriceAskAddres = "v2/histoday";

        private const int _cryptoCurseInfoStringLength = 11;

        public CryptoInfoHistorialGiver(ICryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
        }


        public IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length)
        {
            const int _dayPriceAnswerDayInfoRowUpOffset = 15;
            const int _dayPriceAnswerDayInfoRowDownOffset = 3;

            if (_cryptoInfoGiver.ContainsCryptoApiKey(comperable) == false || _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false)
                throw new ArgumentException();

            var parsedAnswer = _cryptoInfoGiver.Parse(_daysPriceAskAddres, GetExtraSourceArguments(comperable, comperer, length));

            for (int i = _dayPriceAnswerDayInfoRowUpOffset; i < parsedAnswer.Count - _dayPriceAnswerDayInfoRowDownOffset; i += _cryptoCurseInfoStringLength)
            {
                yield return ParseInfo(parsedAnswer, i);
            }
        }

        public IEnumerable<CryptoCurseInfo> GetHourPrice(Crypto comperable, Crypto comperer, uint length)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CryptoCurseInfo> GetMinutePrice(Crypto comperable, Crypto comperer, uint length)
        {
            throw new NotImplementedException();
        }

        public double GetDaysPrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        public double GetHourPrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        public double GetMinutePrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        private string GetExtraSourceArguments(Crypto comperable, Crypto comperer, uint length)
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
