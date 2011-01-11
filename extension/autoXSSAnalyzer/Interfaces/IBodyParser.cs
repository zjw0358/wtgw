using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public interface IBodyParser {
         MatchCollection TokenizeBody(Session s); 
    }
}
