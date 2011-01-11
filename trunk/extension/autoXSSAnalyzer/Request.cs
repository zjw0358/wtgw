using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 


namespace Casaba {
    public class Request : ICloneable{

        private string httpMethod;
        public string HttpMethod {
            get { return httpMethod; }
            set { httpMethod = value; }
        }
     
        private string path;
        public string Path{
            get { return path; }
            set { path = value; }
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
        


        public Request() {
            this.Headers = new Headers(); 
            bodyBytes = new byte[0];
        }

        public string GetBodyEncodedAs(Encoding e) {
            return e.GetString(this.BodyBytes);
        }
        public MatchCollection FindTokenInHeaders(Token t, int sessionId) {
            MatchCollection mc = new MatchCollection();
            foreach (string header in this.headers.Keys) {
                int k = 0;
                foreach (string val in this.headers[header]) {
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
        public string ContentType{
            get {
                try {
                    List<String> contentType = this.headers["Content-Type"];
                    if (contentType.Count > 0) {
                        return contentType[0];
                    }
                } catch (KeyNotFoundException) {
                    return "";
                }
                return "";
            } //There should only be one content-type.. if there is more than one.. wtf?
        }

        public object Clone() {
            Request r = new Request();
            r.path = new string(this.path.ToCharArray());
            r.HttpMethod = this.HttpMethod;

            r.bodyBytes = new byte[this.bodyBytes.Length];
            for (int i = 0; i < this.bodyBytes.Length; i++) {
                r.bodyBytes[i] = this.bodyBytes[i];
            }
            foreach (string key in this.headers.Keys) {
                List<string> values = this.headers[key];
                foreach (string v in values) {
                    r.headers.Add(new string(key.ToCharArray()), new string(v.ToCharArray()));
                }
            }
            return r;
        }
    }
}
