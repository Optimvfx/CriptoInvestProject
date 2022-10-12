using System.Collections.Generic;
using System.Windows.Forms;
using ZedGraph;

namespace GraphVisualizetion
{
	public class GraphVisualizer
	{
		private readonly uint _lineWidth = 3;

		private Form _form;
		private ZedGraphControl _chart;

		private Dictionary<CurveInfo, PointPairList> _curveInfoToCurve;

		public GraphVisualizer(string name, IEnumerable<CurveInfo> curves)
		{
			_form = GetBaseForm();

			_chart = CreateChart(name);

			_curveInfoToCurve = CreateCurves(_chart, curves);

			 ResizeChart();

			_form.Controls.Add(_chart);
		}

		public void Show()
		{
			Application.Run(_form);
		}

		private Dictionary<CurveInfo, PointPairList> CreateCurves(ZedGraphControl chart, IEnumerable<CurveInfo> curves)
		{
			var curveInfoToCurve = new Dictionary<CurveInfo, PointPairList>();

			foreach (var curveInfo in curves)
			{
				curveInfoToCurve[curveInfo] = CreateCurve(chart, curveInfo);
				curveInfo.OnAddPoint += AddPoint;
			}

			return curveInfoToCurve;
		}

		private PointPairList CreateCurve(ZedGraphControl chart, CurveInfo curveInfo)
		{
			var curve = new PointPairList();

			var line = _chart.GraphPane.AddCurve(curveInfo.Name, curve, curveInfo.Color, SymbolType.None);

			foreach(var point in curveInfo.Points)
            {
				curve.Add(point.X, point.OriginalY);
            }

			line.Line.Width = _lineWidth;
			line.Line.IsAntiAlias = true;

			return curve;
		}

		private Form GetBaseForm()
		{
			var form = new Form { WindowState = FormWindowState.Maximized };

			return form;
		}

		private ZedGraphControl CreateChart(string name)
		{
			var chart = new ZedGraphControl() { Dock = DockStyle.Fill };

			ConfigChart(chart, name);

			return chart;
		}

		private void ConfigChart(ZedGraphControl chart, string name)
		{
			chart.GraphPane.Title.Text = name;
			chart.GraphPane.XAxis.Title.Text = "X";
			chart.GraphPane.YAxis.Title.Text = "Y";
			chart.GraphPane.XAxis.Scale.MaxAuto = true;
			chart.GraphPane.XAxis.Scale.MinAuto = true;
			chart.GraphPane.YAxis.Scale.MaxAuto = true;
			chart.GraphPane.YAxis.Scale.MinAuto = true;
		}

        private void ResizeChart()
        {
			_chart.AxisChange();
			_chart.Invalidate();
			_chart.Refresh();
		}

		private void AddPoint(CurveInfo curve, DataPoint addingPoint)
		{
			_curveInfoToCurve[curve].Add(addingPoint.X, addingPoint.OriginalY);

			ResizeChart();
		}
	}
}