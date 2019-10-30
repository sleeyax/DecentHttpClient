using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using DecentHttpClient.exceptions;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Security;

namespace DecentHttpClient
{
    sealed class BouncyTcpClient
    {
        private TcpClient _tcpClient;
        private string _host;
        private int _port;

        public BouncyTcpClient(string host, int port)
        {
           Connect(host, port);
        }

        public BouncyTcpClient() {}

        public void Connect(string host, int port)
        {
            _host = host;
            _port = port;
            Console.WriteLine($"Connecting to {_host}:{_port}...");
            _tcpClient = new TcpClient(host, port);
        }

        /// <summary>
        /// Send a HTTP request to the remote socket
        /// </summary>
        /// <param name="method"></param>
        /// <param name="resource"></param>
        /// <param name="httpVersion"></param>
        /// <param name="client"></param>
        /// <param name="headers"></param>
        /// <returns>Response Stream</returns>
        public Stream SendHttp(string method, string resource, string httpVersion, TlsClient client, HttpHeaders headers)
        {
            TlsClientProtocol protocol = new TlsClientProtocol(_tcpClient.GetStream(), new SecureRandom());
            try
            {
                protocol.Connect(client);
            }
            catch (TlsException)
            {
                throw new ProtocolVersionNotSupported();
            }

            // build protocol info, headers & body
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{method.ToUpper()} {resource} HTTP/{httpVersion}");
            if (!headers.Contains("host"))
                headers.Add("Host", _host);
            sb.AppendLine(headers.ToString());
            // Console.WriteLine(sb.ToString());

            var requestBytes = Encoding.ASCII.GetBytes(sb.ToString());

            Stream stream = protocol.Stream;
            stream.Write(requestBytes, 0, requestBytes.Length);
            stream.Flush();

            return stream;
        }

        public void CloseConnection()
        {
            _tcpClient.Dispose();
            _tcpClient.Close();
        }
    }
}