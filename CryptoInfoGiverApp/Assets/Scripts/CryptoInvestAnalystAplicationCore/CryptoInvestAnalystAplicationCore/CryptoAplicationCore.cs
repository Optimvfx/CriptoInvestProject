using System;
using System.Collections.Generic;
using System.Linq;
using CryptoInfoGiverSpace;
using Extenstions;
using UnityEngine;

namespace CryptoInvestAnalystAplicationCore
{
    [Serializable]
    public class CryptoAplicationDataBase
    {
        [SerializeField] private CryptoInfoGiver _cryptoInfoGiver;
        [SerializeField] private CryptoInfoAllGiver _cryptoAllInfoGiver;

        [SerializeField] private List<CryptoInfoSource> _cryptoInfoSources;

        public event Action<ReadonlyCryptoInfoSource> AddedNewSource;
        public event Action<Forecast> AddedNewForecast;

        public CryptoAplicationDataBase(CryptoInfoAllGiver cryptoInfoAllInfoGiver, CryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
            _cryptoAllInfoGiver = cryptoInfoAllInfoGiver;

            _cryptoInfoSources = new List<CryptoInfoSource>();
        }

        #region Source
        public IEnumerable<CryptoInfoSource> GetTopSourceByForcasts()
        {
            return _cryptoInfoSources.OrderBy(source =>
            {
                if (TryGetForcastsMedianSecces(source, out double median) == false)
                    return double.MinValue;

                return -median;
            });
        }

        public bool TryAddCriptoInfoSource(ReadonlyCryptoInfoSource source)
        {
            if (_cryptoInfoSources.Contains(source.ConvertToCryptoInfoSource()))
                return false;

            foreach (var forecast in source.AllForecasts)
            {
                if (_cryptoInfoGiver.ContainsCryptoApiKey(forecast.Crypto) == false || _cryptoInfoGiver.ContainsCryptoApiKey(forecast.Comparer) == false)
                    return false;
            }

            var newSource = new CryptoInfoSource(source.AllForecasts, source.Name);
            
            _cryptoInfoSources.Add(newSource);

            AddedNewSource?.Invoke(newSource.ConvertToReadOnlyCryptoInfoSource());

            return true;
        }

        public bool TryGetIndexBySource(in ReadonlyCryptoInfoSource searchingSource, out int index)
        {
            index = -1;

            if (_cryptoInfoSources.Contains(searchingSource.ConvertToCryptoInfoSource()) == false)
                return false;

            index = _cryptoInfoSources.IndexOf(new CryptoInfoSource(searchingSource.AllForecasts, searchingSource.Name));

            return true;
        }

        public bool TryGetSourceByIndex(out ReadonlyCryptoInfoSource cryptoInfoSource, in int index)
        {
            cryptoInfoSource = null;

            if (_cryptoInfoSources.OutOfBounds(index))
                return false;

            cryptoInfoSource = _cryptoInfoSources[index].ConvertToReadOnlyCryptoInfoSource();

            return true;
        }

        public bool TryAddForecast(int cryptoInfoSourceIndex, in Forecast forecast)
        {
            if (_cryptoInfoGiver.ContainsCryptoApiKey(forecast.Crypto) == false)
                return false;

            if (_cryptoInfoSources.OutOfBounds(cryptoInfoSourceIndex))
                return false;

            if(_cryptoInfoSources[cryptoInfoSourceIndex].TryAddForecast(forecast))
            {
                AddedNewForecast?.Invoke(forecast);

                return true;
            }

            return false;
        }

        public bool TryGetForcastsMedianSecces(int cryptoInfoSourceIndex, out double medianResult)
        {
            medianResult = 0.0f;

            if (_cryptoInfoSources.OutOfBounds(cryptoInfoSourceIndex))
                return false;

            return TryGetForcastsMedianSecces(_cryptoInfoSources[cryptoInfoSourceIndex], out medianResult);
        }

        public bool TryGetForecastResult(int cryptoInfoSourceIndex, int forecastIndex, out double resultWitchExceptedDifferenceProcent, out double exceptedResult, out double result)
        {
            exceptedResult = 0.0;
            result = 0.0;

            resultWitchExceptedDifferenceProcent = (float)0.0;

            if (_cryptoInfoSources.OutOfBounds(cryptoInfoSourceIndex))
                return false;

            if (_cryptoInfoSources[cryptoInfoSourceIndex].TryGetForecastByIndex(out Forecast forecast, forecastIndex) == false)
                return false;

            return TryGetForecastResult(forecast, out resultWitchExceptedDifferenceProcent, out exceptedResult, out result);
        }

        public bool TryGetForcastsMedianSecces(CryptoInfoSource cryptoInfoSource, out double medianResult)
        {
            medianResult = 0.0;

            var sum = 0.0;
            var forecastCount = 0;

            foreach (var forecast in cryptoInfoSource.AllForecasts)
            {
                if (TryGetForecastResult(forecast, out double result, out double exepted, out double acctual) == false)
                    return false;

                sum += result;
            }

            medianResult = sum / forecastCount;

            return true;
        }

        public bool TryGetForecastResult(Forecast forecast, out double resultProcent, out double exceptedResult, out double result)
        {
            exceptedResult = 0;
            result = 0;
            resultProcent = 0f;

            try
            {
                var maximalDate = forecast.CreationData + forecast.Length;

                if (maximalDate > DateTime.Now)
                    maximalDate = DateTime.Now;

                exceptedResult = forecast.Price;
                result = _cryptoAllInfoGiver.GetDatePrice(forecast.Crypto, forecast.Comparer, maximalDate);

                var difference = exceptedResult - result;

                resultProcent = (-difference / exceptedResult);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion Source

        #region Helping
        public IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length)
        {
            return _cryptoAllInfoGiver.GetDaysPrice(comperable, comperer, length);
        }
        #endregion Helping
    }
}
