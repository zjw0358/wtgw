using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public interface IHeaderParser {
        MatchCollection TokenizeHeaders(Session s); 
    }
}
