using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTester.Utils
{
    public enum Button
    {
        [Description("=")]
        Equal,
        [Description(".")]
        Point,
        [Description("c")]
        Clear,
        [Description("ca")]
        ClearAll
    }

}
