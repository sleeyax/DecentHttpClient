using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DecentHttpClient;
using DecentHttpClient.security;
using Org.BouncyCastle.Crypto.Tls;
using HttpHeaders = DecentHttpClient.HttpHeaders;

namespace Example
{
    class Program
    {
        private static HttpClient _httpClient;

        static void Main(string[] args)
        {
            TestClient();
            Console.ReadKey();
        }

        private static HttpHeaders GetHttpHeaders()
        {
            HttpHeaders h = new HttpHeaders();
            return h;
        }

        private static void TestClient()
        {
            _httpClient = new HttpClient();
            _httpClient.Headers = GetHttpHeaders();
            _httpClient.Settings.TlsClient = new Tls12Client();

            var response = _httpClient.Get(new Uri("https://sleeyax.com"));

            Console.WriteLine($"Protocol: {response.ProtocolVersion}");
            Console.WriteLine($"Body: {response.Body}");
            Console.WriteLine($"Headers: {response.Headers}");
        }
    }
}
