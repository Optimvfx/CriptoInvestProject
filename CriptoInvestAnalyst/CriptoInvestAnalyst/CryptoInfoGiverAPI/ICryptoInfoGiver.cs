using System.Collections.Generic;

namespace CryptoInvestAnalyst
{
    public interface ICryptoInfoGiver
    {

        IEnumerable<CriptoFullInfo> GetAvailableCoins();
    }

    public interface ICriptoInfoPriceGiver
    {
        double GetPrice(Crypto comperable, Crypto comperer);

        IEnumerable<double> GetPrices(Crypto[] comperables, Crypto comperer);
    }

    public enum Crypto
    {
        BTC,
        ETH,
        USD
    }
}
