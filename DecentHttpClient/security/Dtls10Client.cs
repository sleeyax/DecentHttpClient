using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class Dtls10Client : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion => ProtocolVersion.DTLSv10;
    }
}