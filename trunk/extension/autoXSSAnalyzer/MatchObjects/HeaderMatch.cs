using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    /// <summary>
    /// Class that represents a token found in a header. 
    /// </summary>
    public class HeaderMatch : MatchBase{
        
        
        private string headerName;
        public string HeaderName {
            get { return this.headerName; }
            set { this.headerName = value; }
        }
        
        
        public HeaderMatch()
            : base() {
        
        }
        public HeaderMatch(Token t, int offset, int sessionId, string headerName) : base(t, offset, sessionId){
            this.headerName = headerName;
        }
    }
}
