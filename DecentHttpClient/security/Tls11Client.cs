using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class Tls11Client : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion => ProtocolVersion.TLSv11;
    }
}