using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public class JSONRequestBodyParser : ParserBase, IBodyParser {

        public JSONRequestBodyParser()
            : base() {
                base.contentTypePatterns.Clear();
                base.contentTypePatterns.Add("application/json");
        }
        public MatchCollection TokenizeBody(Session s) {
            MatchCollection mc = new MatchCollection();

            string body = s.Request.GetBodyEncodedAs(Encoding.UTF8);

            parseJsonObject(body, mc, 0, s);

            return mc;

        }
        private void parseJsonObject(string json, MatchCollection mc, int sIndex, Session s){
            //this should put us at the seperator for the name/value pairs. 
            int k = json.IndexOf(":", sIndex);
            if (k == -1)
                return; 

            //add one to be at the start of the value
            k++;

            //if the first char in the value is a { then we have another object.. call recursive.. 
            if (json[k] == '{')
            {
                parseJsonObject(json, mc, k, s);
            }
            else
            {
                int x = k;
                //else we can go ahead and handle the value. 
                while (json[x] != '}' && json[x] != ',')
                    x++;

                int len = x - k;


                string token = json.Substring(k, len);
                //if the token starts with a " then we actually have a value "string" value.. add 1 to the "start" and strip the ". 
                if (token[0] == '\"')
                {
                    k++;
                    token = token.Substring(1, token.Length -2);
                    mc.Add(new BodyMatch(new Token(token), k, s.Id));
                }
                k = k + len;
                parseJsonObject(json, mc, k, s); 
                
            }
       


        }
    }
}
