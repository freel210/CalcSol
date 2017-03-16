using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Model
{
    sealed class MainModel
    {
        public decimal First { get; set; }
        public decimal Second { get; set; }
        public decimal Result { get; set; }

        public decimal addition(decimal a, decimal b)
        {
            return a + b;
        }

        public decimal subtraction(decimal a, decimal b)
        {
            return a - b;
        }
        
        public decimal division(decimal a, decimal b)
        {
            return a / b;
        }

        public decimal multiplicate(decimal a, decimal b)
        {
            return a * b;
        }

        public decimal square(decimal a, decimal b)
        {
            return (decimal)Math.Sqrt((double)a);
        }

        public decimal percent(decimal a, decimal b)
        {
            return (a - b)/a;
        }
    }
}
