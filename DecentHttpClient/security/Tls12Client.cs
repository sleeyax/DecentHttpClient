using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class Tls12Client : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion => ProtocolVersion.TLSv12;
    }
}