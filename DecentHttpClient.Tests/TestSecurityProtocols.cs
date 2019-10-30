using System;
using DecentHttpClient.exceptions;
using DecentHttpClient.security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecentHttpClient.Tests
{
    [TestClass]
    public class TestSecurityProtocols
    {
        private readonly string _url = "https://www.howsmyssl.com/a/check";
        private HttpClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new HttpClient();
        }

        [TestMethod]
        public void Protocol_ShouldBeTls10()
        {
            _client.Settings.TlsClient = new Tls10Client();
            var response = _client.Get(new Uri(_url));
            Assert.IsTrue(response.Body.Contains("\"tls_version\":\"TLS 1.0\""));
        }

        [TestMethod]
        public void Protocol_ShouldBeTls11()
        {
            _client.Settings.TlsClient = new Tls11Client();
            var response = _client.Get(new Uri(_url));
            Assert.IsTrue(response.Body.Contains("\"tls_version\":\"TLS 1.1\""));
        }

        [TestMethod]
        public void Protocol_ShouldBeTls12()
        {
            _client.Settings.TlsClient = new Tls12Client();
            var response = _client.Get(new Uri(_url));
            Assert.IsTrue(response.Body.Contains("\"tls_version\":\"TLS 1.2\""));
        }

        [TestMethod]
        [ExpectedException(typeof(ProtocolVersionNotSupported))]
        public void Protocol_ShouldBeSSL30()
        {
            _client.Settings.TlsClient = new Ssl3Client();
            var response = _client.Get(new Uri(_url));
            Assert.IsTrue(response.Body.Contains("\"tls_version\":\"SSL 3.0\""));
        }

        [TestMethod]
        [ExpectedException(typeof(ProtocolVersionNotSupported))]
        public void Protocol_ShouldBeDtls10()
        {
            _client.Settings.TlsClient = new Dtls10Client();
            var response = _client.Get(new Uri(_url));
            Assert.IsTrue(response.Body.Contains("\"tls_version\":\"DTLS 1.0\""));
        }

        [TestMethod]
        [ExpectedException(typeof(ProtocolVersionNotSupported))]
        public void Protocol_ShouldBeDtls12()
        {
            _client.Settings.TlsClient = new Dtls12Client();
            var response = _client.Get(new Uri(_url));
            Assert.IsTrue(response.Body.Contains("\"tls_version\":\"DTLS 1.2\""));
        }
    }
}