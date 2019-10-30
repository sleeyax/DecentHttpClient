using System;
using System.Net;

namespace DecentHttpClient
{
    // TODO: implement WebRequest class for non-https requests
    public class HttpClient : WebRequest
    {
        private readonly BouncyTcpClient _bouncyTcpClient;
        public new HttpHeaders Headers { get; set; }
        public HttpClientSettings Settings { get; set; }

        public HttpClient(HttpClientSettings settings = null, HttpHeaders headers = null)
        {
            Settings = settings ?? new HttpClientSettings();
            Headers = headers ?? new HttpHeaders();
            _bouncyTcpClient = new BouncyTcpClient();
        }

        public HttpResponse Get(Uri uri)
        {
            return Send(uri, "GET");
        }

        public string GetBody(Uri uri)
        {
            return Get(uri).Body;
        }

        /// <summary>
        /// Send a HTTP request to specified URI, using specified HTTP method
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private HttpResponse Send(Uri uri, string method)
        {
            _bouncyTcpClient.Connect(uri.Host, uri.Port);

            var responseStream = _bouncyTcpClient.SendHttp(method.ToUpper(), uri.PathAndQuery, Settings.HttpProtocolVersion, Settings.TlsClient, Headers);
            using (responseStream)
            {
                HttpResponse httpResponse = new HttpResponse(responseStream);
                // TODO: if the URI is the same for the next request, we should leave the connection open
                _bouncyTcpClient.CloseConnection();
                return httpResponse;
            }
        }

        private bool IsEncrypted(Uri uri) => uri.Scheme == "https";
    }
}