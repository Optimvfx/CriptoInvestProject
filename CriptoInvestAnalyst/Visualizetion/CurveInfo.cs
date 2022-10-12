using System;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;

namespace GraphVisualizetion
{
    public class CurveInfo
    {
        public readonly string Name;
        public readonly Color Color;

        private List<DataPoint> _points;

        public IEnumerable<DataPoint> Points => _points;

        public event Action<CurveInfo, DataPoint> OnAddPoint;

        public CurveInfo(string name, Color color)
        {
            Name = name;
            Color = color;

            _points = new List<DataPoint>();
        }

        public void AddPoint(DataPoint point)
        {
            _points.Add(point);

            OnAddPoint?.Invoke(this, point);
        }
    }
}