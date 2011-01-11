using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    interface IResponseParser {
       
        MatchCollection InspectResponse(Session s, Token t);
    }   
}
