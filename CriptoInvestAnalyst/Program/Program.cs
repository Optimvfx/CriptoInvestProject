using CryptoInvestAnalyst;
using GraphVisualizetion;
using ShowCryptoGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var show = new CryptoGraphShower(new Visualizer());

            show.ShowCryptoGraph(new CryptoInfoHistorialGiver(new CryptoInfoGiver()).GetDaysPrice(Crypto.USD, Crypto.BTC, 2000));
            show.ShowCryptoGraph(new CryptoInfoHistorialGiver(new CryptoInfoGiver()).GetDaysPrice(Crypto.USD, Crypto.ETH, 2000));
            show.ShowCryptoGraph(new CryptoInfoHistorialGiver(new CryptoInfoGiver()).GetDaysPrice(Crypto.BTC, Crypto.ETH, 2000));
        }
    }
}
