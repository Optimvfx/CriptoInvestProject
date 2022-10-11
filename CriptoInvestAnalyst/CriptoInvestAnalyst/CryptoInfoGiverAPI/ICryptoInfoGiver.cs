using System.Collections.Generic;

namespace CryptoInvestAnalyst
{
    public interface ICryptoInfoGiver
    {
        double GetPrice(Crypto comperable, Crypto comperer);

        IEnumerable<CriptoFullInfo> GetAvailableCoins();

    }
    public enum Crypto
    {
        BTC,
        ETH,
        USD
    }
}
