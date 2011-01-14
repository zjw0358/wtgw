using System;
using System.Collections.Generic;
using System.Text;


namespace Secsay {
    interface IRequestParser : IHeaderParser, IQueryStringParser, IBodyParser {
        MatchCollection TokenizeRequest(Session oSession);
        MatchCollection TokenizeRequest(Session oSession, bool bTokenizeQS, bool bTokenizeHeaders, bool bTokenizeBody);
    }
}
