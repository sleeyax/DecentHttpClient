using System;

namespace DecentHttpClient.exceptions
{
    public class HttpResponseParseFailure : Exception
    {
        public HttpResponseParseFailure(Exception ex) : base("Failed to parse HTTP response!", ex) {}
    }
}