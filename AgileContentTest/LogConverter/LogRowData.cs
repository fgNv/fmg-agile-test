using System;
using System.Collections.Generic;
using System.Text;

namespace AgileContentTest
{
    public class LogRowData
    {
        public string Provider { get; set; }
        public string HttpMethod { get; set; }
        public string StatusCode { get; set; }
        public string UriPath { get; set; }
        public decimal TimeTaken { get; set; }
        public string ResponseSize { get; set; }
        public string CacheStatus { get; set; }
    }
}
