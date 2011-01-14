using System;
using System.Collections.Generic;
using System.Text;

namespace Secsay {
    public class MatchCollection : List<MatchBase> {
        public List<BodyMatch> GetMatchesInBody() {
            List<BodyMatch> ret = new List<BodyMatch>();
            foreach(MatchBase match in this){
                if (match is BodyMatch) {
                    ret.Add((BodyMatch)match);
                }
            }
            return ret;
        }

        public List<HeaderMatch> GetMatchesInHeaders() {
            List<HeaderMatch> ret = new List<HeaderMatch>();
            foreach (MatchBase match in this) {
                if (match is HeaderMatch) {
                    ret.Add((HeaderMatch)match);
                }
            }
            return ret;
        }
        public List<QueryStringMatch> GetMatchesInQueryString() {
            List<QueryStringMatch> ret = new List<QueryStringMatch>();

            foreach (MatchBase match in this) {
                if (match is QueryStringMatch) {
                    ret.Add((QueryStringMatch)match);
                }
            }
            return ret;
        }
    }
}
