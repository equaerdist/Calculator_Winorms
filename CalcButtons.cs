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
    public partial class CalcButtons : Form
    {
        public CalcButtons(Dictionary<string, EventHandler> actions)
        {
            InitializeComponent();
            InitializeEvents(actions);
        }
        private void InitializeEvents(Dictionary<string, EventHandler> actions)
        {
            foreach (var control in tableLayoutPanel2.Controls)
            {
                if (control is Button)
                {
                    var btton = (Button)control;
                    btton.Click += actions[btton.Name];
                }
            }
        }
        private void Click_ClearAll(object sender, EventArgs e)
        {

        }

        private void Click_Clear(object sender, EventArgs e)
        {

        }

        private void Click_Sqrt(object sender, EventArgs e)
        {

        }

        private void Click_Divide(object sender, EventArgs e)
        {

        }

        private void Click_Multiply(object sender, EventArgs e)
        {

        }

        private void Click_Minus(object sender, EventArgs e)
        {

        }

        private void Click_Plus(object sender, EventArgs e)
        {

        }

        private void Click_Equal(object sender, EventArgs e)
        {

        }

        private void Click_Dot(object sender, EventArgs e)
        {

        }

        private void Click_Nine(object sender, EventArgs e)
        {

        }

        private void Click_Percent(object sender, EventArgs e)
        {

        }

        private void Click_Eight(object sender, EventArgs e)
        {

        }

        private void Click_Seven(object sender, EventArgs e)
        {

        }

        private void Click_(object sender, EventArgs e)
        {

        }
    }
}
