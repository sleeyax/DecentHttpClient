using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class DefaultTlsClient : Org.BouncyCastle.Crypto.Tls.DefaultTlsClient
    {
        public override TlsAuthentication GetAuthentication()
        {
            return new DefaultTlsAuthentication();
        }
    }
}