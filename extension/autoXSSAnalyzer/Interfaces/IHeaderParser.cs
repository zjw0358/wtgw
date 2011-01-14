using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public interface IHeaderParser {
        MatchCollection TokenizeHeaders(Session s); 
    }
}
