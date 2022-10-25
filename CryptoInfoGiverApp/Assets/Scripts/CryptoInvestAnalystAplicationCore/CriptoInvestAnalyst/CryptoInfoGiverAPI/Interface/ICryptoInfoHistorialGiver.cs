using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoInfoGiverSpace
{
    public interface ICryptoInfoHistorialGiver
    {
        IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<CryptoCurseInfo> GetHourPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<CryptoCurseInfo> GetMinutePrice(Crypto comperable, Crypto comperer, uint length);

        double GetDatePrice(Crypto comperable, Crypto comperer, DateTimeOffset date);
    }
}
