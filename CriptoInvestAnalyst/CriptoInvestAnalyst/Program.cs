using System;

namespace CryptoInvestAnalyst
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var g = new CryptoInfoPriceGiver(new CryptoInfoGiver());

            g.GetPrice(Crypto.BTC, Crypto.ETH);

            foreach (var gg in g.GetPrices(new[] { Crypto.USD, Crypto.ETH, Crypto.BTC }, new[] { Crypto.USD, Crypto.ETH, Crypto.BTC }))
            {
                Console.WriteLine("Key" + gg.Key);

                foreach (var hg in gg.Value)
                {
                    Console.WriteLine("Value" + hg);
                }
            }

            Console.ReadLine();
        }
    }
}
