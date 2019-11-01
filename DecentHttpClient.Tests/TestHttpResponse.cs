using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecentHttpClient.Tests
{
    [TestClass]
    public class TestHttpResponse
    {
        private HttpResponse _httpResponse;
        private HttpClient _httpClient;

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = new HttpClient();
            _httpResponse = _httpClient.Get(new Uri("https://httpbin.org/ip"));
        }

        [TestMethod]
        public void Response_ProtocolVersionEquals11()
        {
            Assert.AreEqual(_httpResponse.ProtocolVersion, "1.1");
        }

        [TestMethod]
        public void Response_HasBody()
        {
            Assert.IsNotNull(_httpResponse.Body);
        }

        [TestMethod]
        public void Response_BodyCanBeNull()
        {
            var response = _httpClient.Get(new Uri("https://httpbin.org/status/200"));
            Assert.AreEqual(null, response.Body);
        }

        [TestMethod]
        public void Response_ContentTypeHeaderEqualsApplicationJson()
        {
            Assert.AreEqual(_httpResponse.Headers["Content-Type"], "application/json");
        }

        [TestMethod]
        public void Response_StatusCodeEquals200()
        {
            Assert.AreEqual(_httpResponse.StatusCode, 200);
        }
    }
}
