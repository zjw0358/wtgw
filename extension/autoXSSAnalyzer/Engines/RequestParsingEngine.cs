using System;
using System.Collections.Generic;
using System.Text;
//using Secsay.AutoDecoder;


namespace Secsay {
    public class RequestParsingEngine {
        private List<IRequestParser> parsers;

        private static RequestParsingEngine instance;

        private RequestParsingEngine(){
            parsers = new List<IRequestParser>();
            parsers.Add(new AutoRequestParser());
        }

        public static RequestParsingEngine GetInstance() {
            if(instance == null){
                instance = new RequestParsingEngine();
            }
            return instance;
        }

        public MatchCollection ProcessSession(Session s) {
             return this.parseSession(s);
        }
        private MatchCollection parseSession(Session s) {
            MatchCollection ret = new MatchCollection();
            //Call the appropriate parser here.. I can add logic to call them based off of criteria or other mechanisms.
            //Lets get the default case.. AutoRequestParser
            foreach (IRequestParser parser in parsers) {
                ret.AddRange(parser.TokenizeRequest(s));
            }
            return ret; 
        }
        //below is for dealing with "Request checking" when looking for a token in a request to replace with a test case. 
        public MatchCollection LocateTokensInRequest(Session s, Token canary) {
            MatchCollection retList = new MatchCollection();
            retList.AddRange(s.Request.FindTokenInHeaders(canary, s.Id));
            retList.AddRange(s.Request.FindTokenInBody(canary, s.Id));

            return retList;
        }
    }
}
