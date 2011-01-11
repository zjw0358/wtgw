using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    /// <summary>
    /// This class represents a token found in the request body. or the body of a response. 
    /// </summary>
    public class BodyMatch : MatchBase{

      public BodyMatch(Token t, int offset, int sessionId) : base(t, offset, sessionId){

      }
    }
}
