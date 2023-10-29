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
        public Form1()
        {
            InitializeComponent();
            CurrentColor = Color.FromArgb(85, 40, 253);
            ErrorMessage = string.Empty;
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
            Expression = $"sqrt({Expression}";
            ExpressionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LastExpression = Expression;
            Expression += @"\";
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
                //    try
                //    {
                if (string.IsNullOrEmpty(ErrorMessage))
                    {
                        var calculator = new InfixCalculator.InfixCalculator(Expression);
                        var result = calculator.Calculate();
                        BeginInvoke(new Action(() => Expression = result.ToString()));
                        
                    }
                //}
                //catch (Exception ex)
                //{
                //    BeginInvoke(new Action(() =>
                //    {
                //        ErrorMessage = $"{ex.Message} - ошибка";
                //        ExpressionChanged?.Invoke(this, EventArgs.Empty);
                //    }));
                //}
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

        }
    }
}
