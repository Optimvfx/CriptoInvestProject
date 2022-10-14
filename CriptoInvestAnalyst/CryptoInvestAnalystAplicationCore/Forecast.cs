using CryptoInfoGiverSpace;
using System;

namespace CryptoInvestAnalystAplicationCore
{
    public class Forecast
    {
        private readonly Crypto _crypto;
        private readonly TimeSpan _length;

        private readonly DateTime _creationData;

        private readonly float _price;
        private readonly Crypto _comparable;

        public Crypto Crypto => _crypto;
        public TimeSpan Length => _length;

        public DateTime CreationData => _creationData;

        public float ExceptedPrice => _price;
        public Crypto Comparer => _comparable;

        public Forecast(Crypto crypto, TimeSpan length, DateTime creationData, float price, Crypto comparable)
        {
            _crypto = crypto;
            _length = length;

            _creationData = creationData;

            _price = price;
            _comparable = comparable;
        }
    }
}