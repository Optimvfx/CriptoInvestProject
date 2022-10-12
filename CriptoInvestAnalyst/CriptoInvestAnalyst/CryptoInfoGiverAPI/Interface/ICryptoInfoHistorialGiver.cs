using System;
using System.Collections.Generic;

namespace CryptoInvestAnalyst
{
    public interface ICryptoInfoHistorialGiver
    {
        IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<CryptoCurseInfo> GetHourPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<CryptoCurseInfo> GetMinutePrice(Crypto comperable, Crypto comperer, uint length);

        double GetDaysPrice(Crypto comperable, Crypto comperer, DateTimeOffset date);

        double GetHourPrice(Crypto comperable, Crypto comperer, DateTimeOffset date);

        double GetMinutePrice(Crypto comperable, Crypto comperer, DateTimeOffset date);
    }
}
