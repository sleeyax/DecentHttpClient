using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DecentHttpClient.Tests
{
    [TestClass]
    public class TestHttpMethods
    {
        private HttpClient _httpClient;
        private readonly string _url = "https://httpbin.org";

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = new HttpClient();
        }

        private HttpHeaders BuildPayloadHeaders(string data)
        {
            HttpHeaders headers = new HttpHeaders
            {
                {"Content-Type", "application/x-www-form-urlencoded"},
                { "Content-Length", data.Length.ToString()}
            };
            return headers;
        }

        [TestMethod]
        public void Test_Method_Get()
        {
            var response = _httpClient.Get(new Uri(_url + "/get?a=b"));
            Assert.IsTrue(response.Body.Contains("\"a\": \"b\""));
        }

        [TestMethod]
        public void Test_Method_Post()
        {
            string data = "a=b";
            _httpClient.Headers = BuildPayloadHeaders(data);
            var response = _httpClient.Post(new Uri(_url + "/post"), data);
            Assert.IsTrue(response.Body.Contains("\"a\": \"b\""));
        }

        [TestMethod]
        public void Test_Method_Put()
        {
            string data = "a=b";
            _httpClient.Headers = BuildPayloadHeaders(data);
            var response = _httpClient.Put(new Uri(_url + "/put"), data);
            Assert.IsTrue(response.Body.Contains("\"a\": \"b\""));
        }

        [TestMethod]
        public void Test_Method_Patch()
        {
            string data = "a=b";
            _httpClient.Headers = BuildPayloadHeaders(data);
            var response = _httpClient.Patch(new Uri(_url + "/patch"), data);
            Assert.IsTrue(response.Body.Contains("\"a\": \"b\""));
        }

        [TestMethod]
        public void Test_Method_Delete()
        {
            string data = "a=b";
            _httpClient.Headers = BuildPayloadHeaders(data);
            var response = _httpClient.Delete(new Uri(_url + "/delete"), data);
            Assert.IsTrue(response.Body.Contains("\"a\": \"b\""));
        }
    }
}