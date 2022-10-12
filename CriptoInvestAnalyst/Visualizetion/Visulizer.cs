using System;
using System.Drawing;

namespace GraphVisualizetion
{
    public class Visualizer
    {
        private readonly Func<uint, uint> _getDeffaultXNextPosition = (y) => (y * 2) + 1;

        public event Action<uint, uint> VisualizationProgresChanged;

        public void ShowGraph(string name, params CurveInfo[] curves)
        {
            var graph = new GraphVisualizer(name, curves);
            graph.Show();
        }

        public void AddPointsToCurve(CurveInfo curve, uint showDistance, Func<uint, double> getYPoistion, Func<uint, uint> getNextXPosition = null)
        {
            getNextXPosition = getNextXPosition ?? _getDeffaultXNextPosition;

            for(uint i = 0; i < showDistance; i = getNextXPosition(i))
            {
                curve.AddPoint(new DataPoint(i, getYPoistion(i)));

                VisualizationProgresChanged?.Invoke(i, showDistance);
            }
        }
    }
}
