using System;
using System.Collections.Generic;

namespace CryptoInfoGiverSpace
{
    public interface ICryptoInfoAllInfoGiver
    {
        IEnumerable<CryptoCurseInfo> GetDaysPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<CryptoCurseInfo> GetHourPrice(Crypto comperable, Crypto comperer, uint length);

        IEnumerable<CryptoCurseInfo> GetMinutePrice(Crypto comperable, Crypto comperer, uint length);

        double GetDatePrice(Crypto comperable, Crypto comperer, DateTimeOffset date);

        double GetPrice(Crypto comperable, Crypto comperer);

        IEnumerable<double> GetPrices(Crypto[] comperables, Crypto comperer);

        Dictionary<Crypto, List<PriceInfo>> GetPrices(Crypto[] comperables, Crypto[] comperer);
    }
}
