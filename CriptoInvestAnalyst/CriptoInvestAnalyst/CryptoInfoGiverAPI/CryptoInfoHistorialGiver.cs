using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoInvestAnalyst
{
    public class CryptoInfoHistorialGiver : ICryptoInfoHistorialGiver
    {
        private readonly ICryptoInfoGiver _cryptoInfoGiver;

        private const string _daysPriceAskAddres = "v2/histoday";

        public CryptoInfoHistorialGiver(ICryptoInfoGiver cryptoInfoGiver)
        {
            _cryptoInfoGiver = cryptoInfoGiver;
        }


        public IEnumerable<double> GetDaysPrice(Crypto comperable, Crypto comperer, uint length)
        {
            if (_cryptoInfoGiver.ContainsCryptoApiKey(comperable) == false || _cryptoInfoGiver.ContainsCryptoApiKey(comperer) == false)
                throw new ArgumentException();

            var parsedAnswer = _cryptoInfoGiver.Parse(_daysPriceAskAddres, "?fsym=BTC&tsym=USD&limit=10");

            foreach(var row in parsedAnswer)
                Console.WriteLine(row);

            throw new ArgumentException();
        }

        public double GetDaysPrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> GetHourPrice(Crypto comperable, Crypto comperer, uint length)
        {
            throw new NotImplementedException();
        }

        public double GetHourPrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> GetMinutePrice(Crypto comperable, Crypto comperer, uint length)
        {
            throw new NotImplementedException();
        }

        public double GetMinutePrice(Crypto comperable, Crypto comperer, DateTimeOffset date)
        {
            throw new NotImplementedException();
        }
    }
}
