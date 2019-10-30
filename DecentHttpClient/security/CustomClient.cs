using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.security
{
    public class CustomClient : DefaultTlsClient
    {
        public override ProtocolVersion ClientVersion { get; }
        public List<int> CipherSuites;

        /// <summary>
        /// Create a custom TLS/SSL client
        /// </summary>
        /// <param name="protocolVersion">protocol to use</param>
        /// <param name="cipherSuites">list of cipher suites</param>
        public CustomClient(ProtocolVersion protocolVersion, List<int> cipherSuites)
        {
            ClientVersion = protocolVersion;
            CipherSuites = cipherSuites;
        }

        public override int[] GetCipherSuites() => CipherSuites.ToArray();
    }
}