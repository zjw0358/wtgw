using System;
using System.Collections.Generic;
using System.Text;

namespace Casaba {
    public abstract class ParserBase {

        protected List<string> contentTypePatterns;

        public List<string> ContentTypePatterns {
            get { return contentTypePatterns; }
        }

        public ParserBase() {
            this.contentTypePatterns = new List<string>();
            this.contentTypePatterns.Add(".*");
        }
    }
}
