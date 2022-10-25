using CryptoInfoGiverApi;
using CryptoInfoGiverApi.JSON.Models;
using Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = new CryptoInfoGiver().GetDayPairOHLCVbyTS(CryptoInfoGiverApi.BaseClass.Currency.USD, new[] { CryptoInfoGiverApi.BaseClass.Currency.BTC }, DateTime.Now);

            Console.WriteLine(value.Result["USD"]["BTC"]);

            Console.ReadLine();
        }
    }
}
