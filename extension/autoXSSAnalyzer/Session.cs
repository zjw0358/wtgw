using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public class Session {
        
        private string host;
        public string Host {
            get { return host; }
            set { host = value; }
        }
       
        private Request request;
        public  Request Request {
            get { return request; }
            set { request = value; }
        }

        private Response response;
        public Response Response {
            get { return response; }
            set { response = value; }
        }

        private Dictionary<string,string> flags;
        public Dictionary<string, string> Flags {
            get { return flags; }
            set { flags = value; }
        }

        private int id;
        public int Id {
            get { return id; }
            set { id = value; }
        }

        private string uriScheme;
        public string UriScheme {
            get { return uriScheme; }
            set { uriScheme = value; }
        }


        private bool containsCodePoint;

        public bool ContainsCodePoint
        {
            get { return containsCodePoint; }
            set { containsCodePoint = value; }
        }

        private UAUnicodeChar chr;

        public UAUnicodeChar Chr
        {
            get { return chr; }
            set { chr = value; }
        }

        private Fiddler.Session fsession;
        public Fiddler.Session Fsession
        {
            get { return fsession; }
            set { fsession = value; }
        }
        public Session() {
            Response = new Response();
            Request = new Request();
            host = "";
            Flags = new Dictionary<string, string>();
            this.containsCodePoint = false;
            
        }

        public Session deepClone() {
            Session s = new Session();
            s.Host = new String(this.Host.ToCharArray());
            s.Id = this.Id;
            s.Request = (Request)this.Request.Clone();
            s.Response = this.response;
            s.UriScheme = this.UriScheme;
            
            foreach (string key in this.flags.Keys) {
                string value = "";
                try {
                    this.flags.TryGetValue(key, out value);
                } catch { }
                s.flags.Add(new string(key.ToCharArray()), new string(value.ToCharArray()));
            }
            return s;
        }
    }
}
