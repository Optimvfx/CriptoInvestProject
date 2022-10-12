using System;

namespace CryptoInvestAnalyst
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var g = new CryptoInfoHistorialGiver(new CryptoInfoGiver());
            foreach (var gg in g.GetDaysPrice(Crypto.BTC, Crypto.USD, 20))
            {

            }

            Console.ReadLine();
        }
    }
}
