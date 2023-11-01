using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public struct PlotPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public PlotPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public interface IPlot
    {
        List<PlotPoint> Values { get; }
        void GetExpression(string postfixExpr);
    }
}
