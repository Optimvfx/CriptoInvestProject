using System;

namespace CryptoInvestAnalyst
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var g = new CryptoInfoGiver.PriceGiver();

            g.GetPrice(Crypto.BTC, Crypto.ETH);

            foreach (var gg in g.GetPrices(new[] { Crypto.USD, Crypto.ETH, Crypto.BTC }, Crypto.ETH))
                Console.WriteLine(gg);

            Console.ReadLine();
        }
    }
}
