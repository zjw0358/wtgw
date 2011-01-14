using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public interface IQueryStringParser {
        MatchCollection TokenizeQueryString(Session s); 
    }
}
