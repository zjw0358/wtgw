using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public interface IQueryStringParser {
        MatchCollection TokenizeQueryString(Session s); 
    }
}
