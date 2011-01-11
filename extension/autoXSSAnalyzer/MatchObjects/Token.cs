using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public class Token {

        private string identifier;
        public string Identifier {
            get { return this.identifier; }
            set { this.identifier = value;}
        }

        public int TokenLength {
            get { return this.identifier.Length; }
        }

        public Token(string token) {
            this.identifier = token;
         
        }
    }
}
