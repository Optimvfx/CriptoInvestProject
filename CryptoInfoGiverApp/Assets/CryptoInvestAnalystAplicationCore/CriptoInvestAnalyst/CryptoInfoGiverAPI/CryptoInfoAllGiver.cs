using System;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoInfoGiverSpace 
{
    [Serializable]
    public class CryptoInfoAllGiver : ICryptoInfoAllInfoGiver
    {
        [SerializeField] private CryptoInfoPriceGiver _priceGiver;
        [SerializeField] private CryptoInfoHistorialGiver _historialGiver;

        public CryptoInfoAllGiver(CryptoInfoPriceGiver priceGiver, CryptoInfoHistorialGiver historialGiver)
        {
            _priceGiver = priceGiver;
            _historialGiver = historialGiver;
        }

        public double GetDatePrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            Debug.Log(_historialGiver == null);
            return _historialGiver.GetDatePrice(comperable, comperer, date);
        }

        public IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length)
        {
            return _historialGiver.GetDaysPrice(comperable, comperer, length);
        }

        public IEnumerable<CryptoCurseInfo> GetHourPrice(Crypto comperable, Crypto comperer, uint length)
        {
            return _historialGiver.GetHourPrice(comperable, comperer, length);
        }

        public IEnumerable<CryptoCurseInfo> GetMinutePrice(Crypto comperable, Crypto comperer, uint length)
        {
            return _historialGiver.GetMinutePrice(comperable, comperer, length);
        }

        public double GetPrice(Crypto comperable, Crypto comperer)
        {
            return _priceGiver.GetPrice(comperable, comperer);
        }

        public IEnumerable<double> GetPrices(Crypto[] comperables, Crypto comperer)
        {
            return _priceGiver.GetPrices(comperables, comperer);
        }

        public Dictionary<Crypto, List<PriceInfo>> GetPrices(Crypto[] comperables, Crypto[] comperers)
        {
            return _priceGiver.GetPrices(comperables, comperers);
        }
    }
}
