using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CalculatorPresenter
    {
        private ICalculator _model;
        public CalculatorPresenter(ICalculator model)
        {
            _model = model;
            _model.ExpressionChanged += validateExpression;
        }
        private void applyErrorColor()
        {
            _model.CurrentColor = Color.FromArgb(255, 85, 85);
            _model.Font = new Font("Segoe UI", 23F, FontStyle.Regular);
        }
        private void validateExpression(object sender, EventArgs args)
        {
            var regex = new Regex(@"\b\d+\.\d+\b");
            var nullRegex = new Regex(@"\\(\s*)0");
            if (!string.IsNullOrEmpty(_model.ErrorMessage))
            {
                _model.Expression = _model.ErrorMessage;
                applyErrorColor();
            }
            _model.ErrorMessage = string.Empty;
            _model.CurrentColor = Color.FromArgb(87, 40, 253);
            _model.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            if (_model.Expression != null && _model.Expression.Contains("%") && regex.IsMatch(_model.Expression)) 
            {
                _model.Expression = "Нельзя делить нацело число с плавающей точкой";
               applyErrorColor();
            }
            if(_model.Expression != null && nullRegex.IsMatch(_model.Expression))
            {
                _model.Expression = "Нельзя делить на 0";
                applyErrorColor();
            }
        }
    }
}
