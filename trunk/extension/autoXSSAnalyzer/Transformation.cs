using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay
{
    [Flags]
    public enum Transformation
    {
        None               = 1, 
        Replacement        = 2, 
        Transformed        = 4, 
        Encoded            = 8,
        ShortestForm       = 16,
        Persistant        = 64,
        Unknown            = 32

    }
}
