using System;

namespace CryptoInvestAnalyst
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var g = new CryptoInfoGiver();

            var result = g.GetPrice(Crypto.BTC, Crypto.USD);

            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
