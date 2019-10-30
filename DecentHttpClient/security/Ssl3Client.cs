using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class Ssl3Client : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion => ProtocolVersion.SSLv3;
    }
}