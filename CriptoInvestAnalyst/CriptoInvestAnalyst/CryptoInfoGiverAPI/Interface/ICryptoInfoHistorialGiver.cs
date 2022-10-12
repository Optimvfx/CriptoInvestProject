using System;
using System.Collections.Generic;

namespace CryptoInvestAnalyst
{
    public interface ICryptoInfoHistorialGiver
    {
        IEnumerable<double> GetDaysPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<double> GetHourPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<double> GetMinutePrice(Crypto comperable, Crypto comperer, uint length);

        double GetDaysPrice(Crypto comperable, Crypto comperer, DateTimeOffset date);

        double GetHourPrice(Crypto comperable, Crypto comperer, DateTimeOffset date);

        double GetMinutePrice(Crypto comperable, Crypto comperer, DateTimeOffset date);
    }
}
