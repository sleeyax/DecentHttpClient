using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class Tls10Client : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion => ProtocolVersion.TLSv10;
    }
}