using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public class ResponseResult {
        MatchBase match;
        public MatchBase Match {
            get { return match; }
            set { match = value; }
        }
        UnicodeTestCase testCase;

        public UnicodeTestCase TestCase
        {
            get { return testCase; }
            set { testCase = value; }
        }


        Transformation transformation;
        public Transformation Transformation 
        {
            get { return this.transformation; }
            set { this.transformation = value; }
        }
        UAUnicodeChar chr;
        public UAUnicodeChar Chr
        {
            get { return chr; }
            set { chr = value; }
        } 
        public string CodePoint
        {
            get { return String.Format("{0:x}", chr.CodePoint); }
            
        }
        string context;
        public string Context {
            get { return context; }
            set { context = value; }
        } 

        public ResponseResult(MatchBase mb) {
            this.match = mb;
            this.transformation = Transformation.None;
      
        }
    }
}
