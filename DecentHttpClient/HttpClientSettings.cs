using DecentHttpClient.security;

namespace DecentHttpClient
{
    public class HttpClientSettings
    {
        public string HttpProtocolVersion { get; set; } = "1.1";
        public DefaultTlsClient TlsClient { get; set; } = new DefaultTlsClient();
    }
}