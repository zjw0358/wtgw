using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public class MatchBase  {

        private Token token;
        public Token Token {
            get { return token; }
            set { token = value; }
        }

        private int offset;
        public int Offset {
            get { return offset; }
            set { offset = value; }
        }
        private int sessionId;

        public int SessionId {
            get { return sessionId; }
            set { sessionId = value; }
        }
        public MatchBase() {

        }
        public MatchBase(Token t, int offset, int sessionId) {
            this.token = t;
            this.Offset = offset;
            this.SessionId = sessionId; 
        }
    }
}
