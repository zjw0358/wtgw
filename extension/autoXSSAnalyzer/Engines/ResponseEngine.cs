using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Secsay {
    public class ResponseEngine {
        private List<IResponseParser> parsers; 
        private static ResponseEngine instance;

        private ResponseEngine(){
            this.parsers = new List<IResponseParser>();
            this.parsers.Add(new ResponseParser());   
        }

        public static ResponseEngine GetInstance(){
            if(instance == null){
                instance = new ResponseEngine();
            }
            return instance;
        }
        public MatchCollection FindTokenMatchesInResponse(Session s, Token t) {
            MatchCollection mc = null;
            foreach (IResponseParser parser in parsers) {
                mc = parser.InspectResponse(s, t);
            }
            return mc;
        }
        private Transformation DeduceTransformation(string context, UnicodeTestCase tc, string canary)
        {
            
            //If the or clause is hit that also means that encoding occured.. we might want to display that info
            //at some point.. *Shrug*
            if (context.Contains(canary + tc.SourcePoint.Chr)){
            
                return Transformation.None;
            
            }else if(DoesRegexMatch(context, tc.CreateSourceRepresentationsRegex(), canary)){
                
                return Transformation.Encoded;
            
            }else if (tc.Target != null && context.Contains(canary + tc.Target.Chr)){

                return Transformation.Transformed;
            
            }else if(tc.Target != null && DoesRegexMatch(context, tc.CreateTargetRepresentationsRegex(), canary)){
                //Should this be Normalization (Now transformation of some kind happend with encoding). 
                return Transformation.Transformed | Transformation.Encoded;
            
            }
            //Was replaced by the UNICODE and other Suggested REplacement Chars Such as ; ? 0xFFFD etc. 
            foreach( UAUnicodeChar ch in UnicodeTestCase.GetReplacementCharList()){

                if (context.Contains(canary + ch.Chr))
                {
                    return Transformation.Replacement;
                }
            }

            foreach (UAUnicodeChar ch in UnicodeTestCase.GetReplacementCharList())
            {
                if (DoesRegexMatch(context, UnicodeTestCase.CreateRegexFromChar(ch), canary))
                {
                    return Transformation.Replacement | Transformation.Encoded;
                }
            }
            return Transformation.Unknown;
        }

        private bool DoesRegexMatch(string target, string rx)
        {
            Regex regex = new Regex(rx, RegexOptions.IgnoreCase);
            Match m = regex.Match(target);

            if (m != null && m.Success)
            {
                return true;
            }
            return false;
        }

        private bool DoesRegexMatch(string target, string rx, string canary)
        {
            return this.DoesRegexMatch(target, canary + rx);
        }
        /// <summary>
        /// This method is where result "confidence" is calculated. A list of all the occurences of the "canary" should be passed in. 
        /// </summary>
        /// <param name="s">Session object with request/response</param>
        /// <param name="matches">These are the canary matches..</param>
        /// <param name="lookAheadFromMatchLoc"></param>
        /// <returns></returns>
        public List<ResponseResult> CalcResults(Session s, MatchCollection matches,  UnicodeTestCases mappings, string canary,int lookAheadFromMatchLoc) {
            List<ResponseResult> ret = new List<ResponseResult>();

            //BUGBUG: Chris says he's getting an exception because the unicode char is not being associated with the session.. 
            
            if (s.Chr == null)
            {
                return ret;
            }
        
            foreach (ResponseHeaderMatch hMatch in matches.GetMatchesInHeaders()) {
                ResponseResult rr = new ResponseResult(hMatch); 
                //Get the match context in each header. Multipule headers could have the same key.. so 
                //a list is returned that contains each "string value" to a given key. Each element representing a different 
                //value for the same key. 

                
                foreach(string headerValue in s.Response.Headers[hMatch.HeaderName]){
                   
                    int forwardDif = 0;

                    int offset = hMatch.Offset + hMatch.Token.TokenLength + lookAheadFromMatchLoc;
                    if (offset  > headerValue.Length) {
                        forwardDif = offset - headerValue.Length;
                    }
                    string context = headerValue.Substring(hMatch.Offset - 4, hMatch.Token.TokenLength + lookAheadFromMatchLoc - forwardDif);
                  
                    UnicodeTestCase mapping = mappings.GetMappingFromSourceCodePoint(s.Chr.CodePoint);
                    // At this point i have the context of the "canary" + 5 chars ahead.. this is where logic can be introduced
                    // to determine "the type of match"
                    rr.Chr = mapping.SourcePoint;
                    rr.Transformation = this.DeduceTransformation(context, mapping, canary);
                    rr.Context = context;
                    rr.TestCase = mapping;
                    //Add to result list
                    System.Diagnostics.Debug.Write(s.Fsession.fullUrl);
                    ret.Add(rr); 
                }
            }
            
            //This will change.. the response should be responsible for returning bytes based off detected encoding..
            //That logic will be moved into the Response class at a later point.
            string body = Encoding.UTF8.GetString(s.Response.BodyBytes);
            
            //Now we find matches in the body.. 
            foreach (BodyMatch bMatch in matches.GetMatchesInBody()) {
                ResponseResult rr = new ResponseResult(bMatch);
                //Get the match context.
                int dif = 0;
                int offset = bMatch.Offset + lookAheadFromMatchLoc + bMatch.Token.TokenLength;

                if (offset > body.Length) {
                    dif = offset - body.Length;
                }

                string context = body.Substring(bMatch.Offset - 4, bMatch.Token.TokenLength + lookAheadFromMatchLoc - dif);
            

                UnicodeTestCase mapping = mappings.GetMappingFromSourceCodePoint(s.Chr.CodePoint);

                rr.Context = context;
                rr.Chr = mapping.SourcePoint;
                rr.Transformation = this.DeduceTransformation(context, mapping, canary);
                rr.TestCase = mapping;

                //System.Diagnostics.Debug.Write(s.Fsession.fullUrl);
                ret.Add(rr);
           
            }
              //Add to result list. 
            return ret;
        }
    }
}
