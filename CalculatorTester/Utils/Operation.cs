using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTester.Utils
{
    public enum Operation
    {
        [Description("+")]
        Add,
        [Description("-")]
        Subtract,
        [Description("*")]
        Multiply,
        [Description("/")]
        Divide
    }
}
