using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class Dtls12Client : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion => ProtocolVersion.DTLSv12;
    }
}