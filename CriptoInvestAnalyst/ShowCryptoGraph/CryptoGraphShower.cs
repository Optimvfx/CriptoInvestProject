using CryptoInvestAnalyst;
using GraphVisualizetion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShowCryptoGraph
{
    public class CryptoGraphShower
    {
        private readonly Visualizer _visualizer;

        public CryptoGraphShower(Visualizer visualizer)
        {
            _visualizer = visualizer;
        }

        public void ShowCryptoGraph(IEnumerable<CryptoCurseInfo> cryptoCurseInfos)
        {
            var crytiCurvseInfosArray = cryptoCurseInfos.ToArray();

            var cryptoMaximalCurve = new CurveInfo("Max", Color.Orange);

            var currentMaximalPriceYIndex = -1;

            _visualizer.AddPointsToCurve(cryptoMaximalCurve, (uint)crytiCurvseInfosArray.Length,
                (y) =>
                {
                    currentMaximalPriceYIndex++;
                    return crytiCurvseInfosArray[currentMaximalPriceYIndex].MaximalPrice;
                },
                (x) =>
                {
                    return x + 1;
                });

            var cryptoMinumalCurve = new CurveInfo("Min", Color.Blue);

            var currentMinimalPriceYIndex = -1;

            _visualizer.AddPointsToCurve(cryptoMinumalCurve, (uint)crytiCurvseInfosArray.Length,
                (x) =>
                {
                    currentMinimalPriceYIndex++;
                    return crytiCurvseInfosArray[currentMinimalPriceYIndex].MinimalPrice;
                },
                (x) =>
                {   
                    return x + 1;
                });

            _visualizer.ShowGraph("Curse", cryptoMaximalCurve, cryptoMinumalCurve);
        }
    }
}
