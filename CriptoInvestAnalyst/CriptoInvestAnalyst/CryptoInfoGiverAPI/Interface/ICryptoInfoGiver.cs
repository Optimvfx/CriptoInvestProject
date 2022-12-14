using System.Collections.Generic;

namespace CryptoInfoGiverSpace
{
    public interface ICryptoInfoGiver
    {
        string GetCryptoCollectionApiCode(IEnumerable<Crypto> cryptos);

        List<string> Parse(string addres, string extraArguments = "");

        bool ContainsCryptoApiKey(Crypto crypto);

        string CryptoToApiName(Crypto crypto);
    }
}
