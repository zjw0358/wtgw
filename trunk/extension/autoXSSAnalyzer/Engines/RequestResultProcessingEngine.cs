using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Secsay {
    public class ResultProcessingEngine {

        static ushort TokenId = 0x0000;
        /// <summary>
        /// This method creates the session list to be injected.. 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="queryStringMatches"></param>
        /// <param name="t">This token is the replacement value..</param>
        /// <param name="encodeQueryStringParams"></param>
        /// <returns></returns>
        public static List<Session> ProcessQueryStringResults(Session s, List<QueryStringMatch> queryStringMatches, Token tt, bool encodeQueryStringParams){
            List<Session> ret = new List<Session>();
            foreach (QueryStringMatch match in queryStringMatches) {
                //HACK: This is to support token Id's infront of the canary for tracing back persistent Xss vulns. 
                string id = String.Format("{0:x4}", ResultProcessingEngine.TokenId);
                Token t = new Token(id + tt.Identifier);
                ResultProcessingEngine.TokenId++;
                

                Session newSession = s.deepClone();
                StringBuilder sb = new StringBuilder(newSession.Request.Path);
                

                if (match.Token.TokenLength > 0) {
                    sb.Replace(match.Token.Identifier, t.Identifier, match.Offset, match.Token.TokenLength);
                } else {
                    sb.Insert(match.Offset, t.Identifier);
                }
                
                newSession.Request.Path = sb.ToString();
                
                ret.Add(newSession);
            }
            return ret;
        }
        public static List<Session> ProcessBodyResults(Session s, List<BodyMatch> bodyMatches, Token tt) {
            List<Session> ret = new List<Session>();
            foreach (BodyMatch match in bodyMatches) {
                //HACK: This is to support token Id's infront of the canary for tracing back persistent Xss vulns. 
                string id = String.Format("{0:x4}", ResultProcessingEngine.TokenId);
                Token t = new Token(id + tt.Identifier);
                ResultProcessingEngine.TokenId++;

                Session newSession = s.deepClone();
                StringBuilder sb = new StringBuilder(newSession.Request.GetBodyEncodedAs(Encoding.UTF8));


                if (match.Token.TokenLength > 0) {
                    sb.Replace(match.Token.Identifier, t.Identifier, match.Offset, match.Token.TokenLength);
                } else {
                    sb.Insert(match.Offset, t.Identifier);
                }

                newSession.Request.BodyBytes = Encoding.UTF8.GetBytes(sb.ToString());

                ret.Add(newSession);
            }

            return ret;
        }
        public static List<Session> ProcessHeaderResults(Session s, List<HeaderMatch> matches, Token tt) {
            List<Session> ret = new List<Session>();
            foreach (HeaderMatch match in matches) {
                Session newSession = s.deepClone();
                List<string> headersForKey = s.Request.Headers[match.HeaderName];

                foreach (string header in headersForKey) {
                    //HACK: This is to support token Id's infront of the canary for tracing back persistent Xss vulns. 
                    string id = String.Format("{0:x4}", ResultProcessingEngine.TokenId);
                    Token t = new Token(id + tt.Identifier);
                    ResultProcessingEngine.TokenId++;



                    StringBuilder sb = new StringBuilder(header);

                    if (match.Token.TokenLength > 0) {
                        sb.Replace(match.Token.Identifier, t.Identifier, match.Offset, match.Token.TokenLength);
                    } else {
                        sb.Insert(match.Offset, t.Identifier);
                    }

                    headersForKey.Remove(header);
                    headersForKey.Add(sb.ToString());
                }
                ret.Add(newSession);
            }
            return ret;
        }
    }
}
