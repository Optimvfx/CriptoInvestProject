using CryptoInfoGiverSpace;
using System;
using UnityEngine;

namespace CryptoInvestAnalystAplicationCore
{
    [Serializable]
    public struct Forecast
    {
        [SerializeField] private Crypto _crypto;
        [SerializeField] private TimeSpan _length;

        [SerializeField] private DateTime _creationData;

        [SerializeField] private float _price;
        [SerializeField] private Crypto _comparer;

        public Crypto Crypto => _crypto;
        public TimeSpan Length => _length;

        public DateTime CreationData => _creationData;

        public float Price => _price;

        public Crypto Comparer => _comparer;

        public Forecast(Crypto crypto, TimeSpan length, DateTime creationData, UFloat price, Crypto comparable)
        {
            _crypto = crypto;
            _length = length;

            _creationData = creationData;

            _price = (float)price;
            _comparer = comparable;
        }

        public override bool Equals(object obj)
        {
            if (obj is Forecast == false)
                return false;

            var other = (Forecast)obj;

            return other.Crypto == _crypto && other.Length == _length && other.CreationData == _creationData && other.Price == _price && other.Comparer == _comparer;
        }
    }
}