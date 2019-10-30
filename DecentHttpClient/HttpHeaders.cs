using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace DecentHttpClient
{
    public class HttpHeaders : System.Net.Http.Headers.HttpHeaders
    {
        public void AddRange(Dictionary<string, string> headers)
        {
            foreach (string key in headers.Keys)
            {
                Add(key, headers[key]);
            }
        }
    }
}