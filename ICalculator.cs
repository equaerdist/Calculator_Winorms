using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface ICalculator
    {
        string LastExpression { get; set; }
        string Expression { get; set; }
        event EventHandler ExpressionChanged;
        Color CurrentColor { get; set; }
        string ErrorMessage { get; set; }
        Font Font { get; set; }
    }
}
