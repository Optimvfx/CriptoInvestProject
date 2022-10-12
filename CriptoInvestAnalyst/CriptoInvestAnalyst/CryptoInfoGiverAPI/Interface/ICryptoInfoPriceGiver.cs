﻿using System.Collections.Generic;

namespace CryptoInvestAnalyst
{
    public interface ICriptoInfoPriceGiver
    {
        double GetPrice(Crypto comperable, Crypto comperer);

        IEnumerable<double> GetPrices(Crypto[] comperables, Crypto comperer);

        Dictionary<Crypto, List<PriceInfo>> GetPrices(Crypto[] comperables, Crypto[] comperer);
    }
}