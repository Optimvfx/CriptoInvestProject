using System.Collections.Generic;

namespace CryptoInvestAnalyst
{
    public interface ICryptoInfoHistorialGiver
    {
        IEnumerable<double> GetDaysPrice(Crypto comperable, Crypto comperer, uint length);
    }
}
