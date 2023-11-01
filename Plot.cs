using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class Plot : Form, IPlot
    {
        private readonly Action<string> SetError;
        public Plot(Action<string> setError)
        {
            InitializeComponent();
            Values = new List<PlotPoint>
            {
                new PlotPoint(0, 0)
            };
            SetError = setError;
            chart1.Series[0].LegendText = "Y by X";
            chart1.Series[0].Points.DataBind(Values, "X", "Y", "");
            chart1.Visible = false;
        }

        public List<PlotPoint> Values { get; }

        public void GetExpression(string infixExpr)
        {
            if (infixExpr.Contains("x")) 
            {
                Task.Run(() =>
                {
                    lock (Values)
                    {
                       
                        Values.Clear();
                        var calc = new InfixCalculator.InfixCalculator(infixExpr);
                        var start = -Math.PI;
                        var end = Math.PI;
                        var stepAmount = 75;
                        var step = (end - start) / stepAmount;
                        while (start <= end)
                        {
                            try
                            {
                                var y = calc.Calculate(start);
                                Values.Add(new PlotPoint(start, y));
                                start += step;
                            }
                            catch(OverflowException ex)
                            {
                                BeginInvoke(new Action(() => SetError($"Достигнут лимит по доступной величине")));
                            }
                        }
                    }
                }).ContinueWith((task) => 
                {
                    BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            chart1.Visible = true;
                            chart1.DataSource = Values;
                            chart1.Series[0].XValueMember = "X";
                            chart1.Series[0].YValueMembers = "Y";
                            chart1.DataBind();
                        }
                        catch(OverflowException ex)
                        {
                            SetError($"Превышен допустимый лимит вычислений");
                        }
                    }));
                });
               
            }
        }
    }
}
