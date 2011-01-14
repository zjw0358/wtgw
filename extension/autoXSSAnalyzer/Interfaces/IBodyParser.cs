using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public interface IBodyParser {
         MatchCollection TokenizeBody(Session s); 
    }
}
