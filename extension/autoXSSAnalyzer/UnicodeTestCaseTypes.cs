using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay
{
    [Flags]
    public enum UnicodeTestCaseTypes
    {
        Transformable = 1,
        Traditional = 2,
        Overlong = 4
    }
}