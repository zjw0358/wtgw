using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public class ResponseParser : IResponseParser{
        public MatchCollection InspectResponse(Session s, Token t) {
            MatchCollection retList = new MatchCollection();
            retList.AddRange(s.Response.FindTokenInHeaders(t, s.Id));
            retList.AddRange(s.Response.FindTokenInBody(t, s.Id));
            return retList;
        }
    }   
}
