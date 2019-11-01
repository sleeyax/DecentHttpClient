using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using DecentHttpClient.exceptions;
using Org.BouncyCastle.Asn1;

namespace DecentHttpClient
{
    // TODO: implement all WebResponse properties & methods
    public class HttpResponse : WebResponse
    {
        private readonly Stream _responseStream;
        /*public override long ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override Uri ResponseUri { get; }*/
        public override WebHeaderCollection Headers { get; } = new WebHeaderCollection();
        public string ProtocolVersion { get; set; }
        public int StatusCode { get; set; }
        public string Body { get; set; }


        public HttpResponse(Stream stream)
        {
            _responseStream = stream;
            try
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    // read 1st line
                    string line = sr.ReadLine();
                    ParseHttpInfo(line);

                    // iterate lines
                    while ((line = sr.ReadLine()) != null)
                    {
                        /*Console.WriteLine(line);
                        Console.WriteLine(sr.Peek());
                        Console.WriteLine(line == "");*/

                        if (Regex.IsMatch(line, @"^[a-zA-Z\-]+: .+$", RegexOptions.Multiline))
                        {
                            ParseHeader(line);
                        }
                        else if (line != "")
                        {
                            ParseBody(line);
                        }
                    }
                }
            }
            // NOTE: this is a hacky work-around for the '.Read() stuck for no reason whatsoever, fuck it' bug
            catch (SocketException) {}
            catch(IOException) {}
            // --
            catch (Exception ex)
            {
                throw new HttpResponseParseFailure(ex);
            }

        }

        /// <summary>
        /// Parse the first line of a HTTP response
        /// e.g. HTTP/1.1 200 OK
        /// </summary>
        /// <param name="data"></param>
        private void ParseHttpInfo(string line)
        {
            var splitted = line.Split(' ');
            ProtocolVersion = splitted[0].Split('/')[1];
            StatusCode = int.Parse(splitted[1]);
        }

        /// <summary>
        /// Parse HTTP header
        /// e.g. User-Agent: CURL
        /// </summary>
        /// <param name="line"></param>
        private void ParseHeader(string line)
        {
            var splitted = line.Split(':');
            string headerKey = splitted[0].Trim();
            string headerValue = splitted[1].Trim();
            Headers.Add(headerKey, headerValue);
        }

        /// <summary>
        /// Parse (optional) response body
        /// e.g. Hello World!
        /// </summary>
        /// <param name="line"></param>
        private void ParseBody(string line)
        {
            Body += line.Trim();
        }

        /// <summary>
        /// Returns the full HTTP response stream
        /// </summary>
        /// <returns></returns>
        public override Stream GetResponseStream()
        {
            return _responseStream;
        }

        /// <summary>
        /// Returns the HTTP response body as a stream
        /// </summary>
        /// <returns></returns>
        public Stream GetResponseBodyStream()
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(Body));
        }

        public override string ToString()
        {
            return Body;
        }
    }
}