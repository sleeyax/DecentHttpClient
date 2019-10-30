using System;
using System.Collections.Generic;
using DecentHttpClient.security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto.Tls;

namespace DecentHttpClient.Tests
{
    [TestClass]
    public class TestCipherSuites
    {
        [TestMethod]
        public void Test_CustomCipherSuites()
        {
            var client = new HttpClient();
            client.Settings.TlsClient = new CustomClient(ProtocolVersion.TLSv12, new List<int>
            {
                CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA,
                CipherSuite.TLS_RSA_WITH_3DES_EDE_CBC_SHA
            });

            var response = client.Get(new Uri("https://www.howsmyssl.com/a/check"));

            Assert.IsTrue(response.Body.Contains("\"given_cipher_suites\":[\"TLS_RSA_WITH_AES_128_CBC_SHA\",\"TLS_RSA_WITH_3DES_EDE_CBC_SHA\",\"TLS_EMPTY_RENEGOTIATION_INFO_SCSV\"]"));
        }
    }
}