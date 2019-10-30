using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class DefaultTlsAuthentication : TlsAuthentication
    {
        public void NotifyServerCertificate(Certificate serverCertificate) {}
        public TlsCredentials GetClientCredentials(CertificateRequest certificateRequest) { return null; }
    }
}