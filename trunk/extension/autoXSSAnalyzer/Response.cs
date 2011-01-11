using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Casaba {
    public class Response {

        private string httpMethod;
        public string HttpMethod {
            get { return httpMethod; }
            set { httpMethod = value; }
        }

        private Headers headers;
        public Headers Headers {
            get { return headers; }
            set { headers = value; }
        }
        
        private byte[] bodyBytes;
        public byte[] BodyBytes {
            get { return bodyBytes; }
            set { bodyBytes = value; }
        }

        public Response() {
            this.headers = new Headers(); ;
            this.BodyBytes = new byte[0];
        }
        
        public MatchCollection FindTokenInHeaders(Token t, int sessionId){
            MatchCollection mc = new MatchCollection(); 
            foreach (string header in this.headers.Keys) {
                int k = 0; 
                foreach(string val in this.headers[header]){
                    if (val.Contains(t.Identifier)) {
                        for (int i = header.IndexOf(t.Identifier); i > 0 && i != -1; ) {
                            //so the canary is present, lets muck with it!
                            mc.Add(new ResponseHeaderMatch(t, i, sessionId, header, k));
                            i = header.IndexOf(t.Identifier, i + t.TokenLength);
                        }
                    }
                    k++;
                }
            }
            return mc;   
        }
        
        public MatchCollection FindTokenInBody(Token t, int sessionId) {
            string body = System.Text.Encoding.UTF8.GetString(this.BodyBytes);
            
            MatchCollection mc = new MatchCollection();
            for (int i = body.IndexOf(t.Identifier); i > 0 && i != -1; ) {
                //so the canary is present, lets muck with it!
                mc.Add(new BodyMatch(t, i, sessionId));
                i = body.IndexOf(t.Identifier, i + t.TokenLength);
            }
            return mc;
        }
    }
}
