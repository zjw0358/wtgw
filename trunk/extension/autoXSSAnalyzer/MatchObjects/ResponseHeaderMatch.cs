using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba{
    public class ResponseHeaderMatch : HeaderMatch {

        int index;

        public int Index {
            get { return index; }
            set { index = value; }
        }
        public ResponseHeaderMatch()
            : base() {
            
        }
        public ResponseHeaderMatch(Token t, int offset, int sessionId, string headerName, int index)
            : base(t, offset, sessionId, headerName) {
            this.index = index;
        }
       
    }
}
