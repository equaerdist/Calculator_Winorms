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
    public partial class Form1 : Form, ICalculator
    {
        private CalculatorPresenter _presenter;
        private Form _activeForm;
        private Dictionary<string, EventHandler> _actions;
        public Form1()
        {
            InitializeComponent();
           
             _actions = new Dictionary<string, EventHandler>
            {
                {"ClearAll", button2_Click},
                {"Clear", button3_Click },
                {"Zero", button7_Click },
                {"One", button6_Click },
                {"Two", button8_Click },
                {"Three", button9_Click },
                {"Four", button11_Click },
                { "Five", button12_Click},
                {"Six", button13_Click },
                {"Seven", button14_Click },
                {"Eight", button16_Click },
                {"Nine", button17_Click },
                {"Percent", button22_Click },
                {"Dot", button24_Click },
                {"Divide", button5_Click },
                {"Multiply", button10_Click },
                {"Minus", button15_Click },
                {"Plus", button20_Click },
                {"Equal", button25_Click },
                {"Sqrt", button4_Click },


            };
            var defaultView = new CalcButtons(_actions);
            defaultView.TopLevel = false;
            defaultView.FormBorderStyle = FormBorderStyle.None;
            _activeForm = defaultView;
            defaultView.Dock = DockStyle.Fill;
            defaultView.Show();
            CurrentColor = Color.FromArgb(85, 40, 253);
            ErrorMessage = string.Empty;
            tableLayoutPanel1.Controls.Add(_activeForm, 0, 2);
            Expression = string.Empty;
            LastExpression = string.Empty;
            _presenter = new CalculatorPresenter(this);
        }
        Font ICalculator.Font { get => textBox1.Font; set => textBox1.Font = value; }
        public string Expression { get => textBox1.Text; set => textBox1.Text = value; }
        public string LastExpression { get; set; }

        public event EventHandler ExpressionChanged;

        private void button2_Click(object sender, EventArgs e)
        {   
            LastExpression = Expression;
            Expression = string.Empty;
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
        }
        public Color CurrentColor { get => textBox1.ForeColor; set => textBox1.ForeColor = value; }
        public string ErrorMessage { get; set; }
        private void button3_Click(object sender, EventArgs e)
        {
            if (LastExpression.Equals(Expression))
            {
                if (Expression.Length > 0)
                    Expression = Expression.Remove(Expression.Length - 1);
            }
            else
                Expression = LastExpression;
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LastExpression = Expression;
            Expression = $"sqrt({Expression})";
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LastExpression = Expression;
            Expression += @"/";
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
        }
        private void ModifyExpression(string symbol)
        {
            LastExpression = Expression;
            Expression += symbol;
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            ModifyExpression("*");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ModifyExpression("-");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ModifyExpression("+");
        }

        private async void button25_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(ErrorMessage))
                    {
                        var calculator = new InfixCalculator.InfixCalculator(Expression);
                        var result = calculator.Calculate(null);
                        BeginInvoke(new Action(() => Expression = result.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        ErrorMessage = $"{ex.Message} - ошибка";
                        ExpressionChanged?.Invoke(this, EventArgs.Empty);
                    }));
                }
            });
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ModifyExpression(".");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ModifyExpression("%");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ModifyExpression("9");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ModifyExpression("8");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ModifyExpression("7");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ModifyExpression("6");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ModifyExpression("5");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ModifyExpression("4");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ModifyExpression("3");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ModifyExpression("2");
        }
        private void ActivateButton(Button button)
        {
            button.BackColor = Color.FromArgb(85, 40, 253);
            button.ForeColor = Color.FromArgb(35, 37, 36);
        }
        private void DisactivateButton(Button button)
        {
            button.BackColor = Color.FromArgb(35, 37, 36);
            button.ForeColor = Color.FromArgb(85, 40, 253);
        }
        private void DisactivaeAllButtons()
        {
            foreach(var control in panel2.Controls)
            {
                if (control is Button)
                    DisactivateButton((Button)control);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ModifyExpression("1");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ModifyExpression("0");
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
            if(_activeForm is Plot)
            {
                var plot = (IPlot) _activeForm;
                plot.GetExpression(Expression);
               
            }
        }
        private void ApplyFormSettings(Form form)
        {
            if (_activeForm is CalcButtons && form is CalcButtons)
                return;
            _activeForm?.Close();
            _activeForm = form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
            tableLayoutPanel1.Controls.Add(_activeForm, 0, 2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            var calcView = new CalcButtons(_actions);
            DisactivaeAllButtons();
            ActivateButton((Button)sender);
            ApplyFormSettings(calcView);

        }

        private void button18_Click(object sender, EventArgs e)
        {
            var plotView = new Plot((text) => ErrorMessage = text);
          
            DisactivaeAllButtons();
            ActivateButton((Button)sender);
            ApplyFormSettings(plotView);
            plotView.GetExpression(Expression);
        }
    }
}
