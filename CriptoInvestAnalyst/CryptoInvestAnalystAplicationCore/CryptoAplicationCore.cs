using System;
using System.Collections.Generic;
using System.Linq;
using CryptoInfoGiverSpace;
using Extenstions;

namespace CryptoInvestAnalystAplicationCore
{
    public class CryptoAplicationDataBase
    {
        private readonly ICryptoInfoGiver _cryptoInfoGiver;
        private readonly ICryptoInfoAllInfoGiver _cryptoAllInfoGiver;

        private readonly List<CryptoInfoSource> _cryptoInfoSources;

        public CryptoAplicationDataBase(ICryptoInfoAllInfoGiver cryptoInfoAllInfoGiver, ICryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
            _cryptoAllInfoGiver = cryptoInfoAllInfoGiver;

            _cryptoInfoSources = new List<CryptoInfoSource>();
        }

        public bool TryAddCriptoInfoSource(ReadonlyCryptoInfoSource source)
        {
            foreach (var forecast in source.AllForecasts)
            {
                if (_cryptoInfoGiver.ContainsCryptoApiKey(forecast.Crypto) == false || _cryptoInfoGiver.ContainsCryptoApiKey(forecast.Comparer) == false)
                    return false;
            }

            _cryptoInfoSources.Add(new CryptoInfoSource(source.AllForecasts));
            return true;
        }

        public bool TryGetSourceByIndex(out ReadonlyCryptoInfoSource cryptoInfoSource, in int index)
        {
             cryptoInfoSource = null;

            if (_cryptoInfoSources.OutOfBounds(index))
                return false;

            cryptoInfoSource = _cryptoInfoSources[index];

            return true;
        }

        public bool TryAddForecast(int cryptoInfoSourceIndex, in Forecast forecast)
        {
            if (_cryptoInfoGiver.ContainsCryptoApiKey(forecast.Crypto) == false)
                return false;

            if (_cryptoInfoSources.OutOfBounds(cryptoInfoSourceIndex))
                return false;

            return _cryptoInfoSources[cryptoInfoSourceIndex].TryAddForecast(forecast);
        }

        public bool TryGetForecastResult(int cryptoInfoSourceIndex, int forecastIndex, out  Procent resultWitchExceptedDifferenceProcent, out double exceptedResult, out double result)
        {
            exceptedResult = 0.0;
            result = 0.0;

            resultWitchExceptedDifferenceProcent = (float)0.0;

            if (_cryptoInfoSources.OutOfBounds(cryptoInfoSourceIndex))
                return false;

            if (_cryptoInfoSources[cryptoInfoSourceIndex].TryGetForecastByIndex(out Forecast forecast, forecastIndex) == false)
                return false;

            try
            {
                var maximalDate = forecast.CreationData + forecast.Length;

                if (maximalDate > DateTime.Now)
                    maximalDate = DateTime.Now;

                exceptedResult = forecast.ExceptedPrice;
                result = _cryptoAllInfoGiver.GetDatePrice(forecast.Crypto, forecast.Comparer, maximalDate);

                var difference = exceptedResult - result;

                resultWitchExceptedDifferenceProcent = (Procent)(-difference / exceptedResult);

                return true;
            }
            catch
            {
                return false;   
            }
        }
    }
}
