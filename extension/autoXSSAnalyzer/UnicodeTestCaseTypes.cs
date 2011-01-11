using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba
{
    [Flags]
    public enum UnicodeTestCaseTypes
    {
        Transformable = 1,
        Traditional = 2,
        Overlong = 4
    }
}