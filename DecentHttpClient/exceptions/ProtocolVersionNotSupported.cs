using System;

namespace DecentHttpClient.exceptions
{
    public class ProtocolVersionNotSupported : Exception
    {
        public ProtocolVersionNotSupported() : base("The server doesn't support this TLS/SSL protocol version!") {}
    }
}