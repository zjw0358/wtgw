using System;
using System.Collections.Generic;
using System.Text;


namespace Casaba {
    public class NameValueQueryStringParser : ParserBase, IQueryStringParser{
        public NameValueQueryStringParser()
            : base() {
                base.contentTypePatterns.Clear();
                base.contentTypePatterns.Add(".*");
        }

        public MatchCollection TokenizeQueryString(Session s) {
            MatchCollection mc = new MatchCollection();
            int offsetCount = 0;

            string rawQueryString = s.Request.Path;
            
            string[] tmp = rawQueryString.Split('?');
            if (tmp.Length < 2) throw new ArgumentException();

            //Keep track of the offset. (+1 for the ?)
            offsetCount += tmp[0].Length + 1;

            string[] paramaters = tmp[1].Split('&');

            foreach (string param in paramaters) {
                string[] rawParams = param.Split('=');
                //BUGBUG: Sometimes this returns when  i fail to parse the query string =( i could add better handlings.. maybe another layer of splittings.. i'll think about it.   
                if (rawParams.Length == 0 || rawParams.Length > 2) return mc;
                // if the length is equal to two then we have a name value pair.. otherwise we have a "single" param.
                if (rawParams.Length == 2) {
                    //then add the value as a token along with it's offset.. 
                    offsetCount += rawParams[0].Length + 1;
                    mc.Add(new QueryStringMatch(new Token(rawParams[1]), offsetCount, s.Id));
                    offsetCount += rawParams[1].Length + 1;
                } else {
                    //offset = offsetCount;    
                    //add the token.. 
                    mc.Add(new QueryStringMatch(new Token(rawParams[0]), offsetCount, s.Id));
                    offsetCount += rawParams[0].Length + 1;//the plus one is for the & symbol.. 
                }
            }
            return mc;
        } 
    }
}
