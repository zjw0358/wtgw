using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    interface IResponseParser {
       
        MatchCollection InspectResponse(Session s, Token t);
    }   
}
