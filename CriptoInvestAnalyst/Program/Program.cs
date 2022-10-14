using CryptoInfoGiverSpace;
using GraphVisualizetion;
using ShowCryptoGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoInvestAnalystAplicationCore;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var show = new CryptoGraphShower(new Visualizer());

            var crInfo = new CryptoInfoGiver();
            var chFullInfo = new CryptoInfoAllGiver(new CryptoInfoPriceGiver(crInfo), new CryptoInfoHistorialGiver(crInfo));

            var cryptoAplicationCore = new CryptoAplicationDataBase(chFullInfo, crInfo);

            cryptoAplicationCore.TryAddCriptoInfoSource(new CryptoInfoSource(new[] { new Forecast(Crypto.USD, new TimeSpan(2000, 0, 0, 0), new DateTime(2018, 1, 1), 15000, Crypto.BTC) }));

            cryptoAplicationCore.TryGetForecastResult(0, 0, out Procent result, out double start, out double end);

            show.ShowCryptoGraph(new CryptoInfoHistorialGiver(new CryptoInfoGiverSpace.CryptoInfoGiver()).GetDaysPrice(Crypto.USD, Crypto.ETH, 2000));
            //     show.ShowCryptoGraph(new CryptoInfoHistorialGiver(new CryptoInfoGiverSpace()).GetDatePrice(Crypto.BTC, Crypto.ETH, 2000));
        }
    }
}
