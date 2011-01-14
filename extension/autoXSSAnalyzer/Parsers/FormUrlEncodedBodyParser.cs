using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay{
 
    public class FormUrlEncodedBodyParser : ParserBase, IBodyParser {
        public FormUrlEncodedBodyParser() : 
            base()
        {
            base.contentTypePatterns.Clear();
            base.contentTypePatterns.Add("application/x-www-form-urlencoded");
        }

        
        public MatchCollection TokenizeBody(Session s) {
            MatchCollection mc = new MatchCollection();
            int offsetCount = 0;

            string body = s.Request.GetBodyEncodedAs(Encoding.UTF8);

            string[] paramaters = body.Split('&');

            foreach (string param in paramaters) {
                string[] rawParams = param.Split('=');
                if (rawParams.Length == 0 || rawParams.Length > 2) throw new ArgumentException();
                // if the length is equal to two then we have a name value pair.. otherwise we have a "single" param.
                if (rawParams.Length == 2) {
                    //then add the value as a token along with it's offset.. 
                    offsetCount += rawParams[0].Length + 1;
                    mc.Add(new BodyMatch(new Token(rawParams[1]), offsetCount, s.Id));
                    offsetCount += rawParams[1].Length + 1;
                } else {
                    //offset = offsetCount;    
                    //add the token.. 
                    mc.Add(new BodyMatch(new Token(rawParams[0]), offsetCount, s.Id));
                    offsetCount += rawParams[0].Length + 1;//the plus one is for the & symbol.. 
                }
            }
            return mc;
        }
    }
}
