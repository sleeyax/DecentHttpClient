# DecentHttpClient
'Decent' not as in 'better than', but as in 'just right'.

DecentHttpClient is not a replacement for existing .NET http clients like `WebClient` and `WebRequest`. It's a wrapper, aiming for more customization on the lower levels by adding additional functionalities.

This project should be considered a WIP.

## Features
- Change HTTP version
- TLS/SSL settings
  - Change protocol (TLS v1.1, TLS v1.2, SSL3, ...)
  - Edit cipher suites

## Examples
Some code snippets require the [bouncy castle](https://www.nuget.org/packages/BouncyCastle/) package to support types. 

### Custom client
Fully customized client to send HTTPS requests:
```csharp
// custom list of cipher suites
List<int> cipherSuites = new List<int>
{
    CipherSuite.TLS_RSA_WITH_AES_128_CBC_SHA,
    CipherSuite.TLS_RSA_WITH_AES_256_CBC_SHA,
    CipherSuite.DRAFT_TLS_DHE_PSK_WITH_AES_128_OCB,
    CipherSuite.DRAFT_TLS_DHE_PSK_WITH_AES_256_OCB
};

// custom TLS protocol version
ProtocolVersion protocolVersion = ProtocolVersion.TLSv10;

HttpClientSettings settings = new HttpClientSettings
{
    // custom HTTP version (HTTP/1.0)
    HttpProtocolVersion = "1.0",
    TlsClient = new CustomClient(protocolVersion, cipherSuites)
};

DecentHttpClient.HttpClient client = new DecentHttpClient.HttpClient(settings);
HttpResponse response = client.Get(new Uri("https://www.howsmyssl.com/a/check"));

Console.WriteLine(response.Body);
// response:
/*{
  "given_cipher_suites": [
    "TLS_RSA_WITH_AES_128_CBC_SHA",
    "TLS_RSA_WITH_AES_256_CBC_SHA",
    "Some unknown cipher suite: 0xff12",
    "Some unknown cipher suite: 0xff13",
    "TLS_EMPTY_RENEGOTIATION_INFO_SCSV"
  ],
  "ephemeral_keys_supported": false,
  "session_ticket_supported": false,
  "tls_compression_supported": false,
  "unknown_cipher_suite_supported": true,
  "beast_vuln": false,
  "able_to_detect_n_minus_one_splitting": true,
  "insecure_cipher_suites": {},
  "tls_version": "TLS 1.0",
  "rating": "Bad"
}*/
```
### Changing settings per request
You can change the `HttpClientSettings` at runtime by using the `HttpClient.Settings` property:
```csharp
// ...
DecentHttpClient.HttpClient client = new DecentHttpClient.HttpClient(settings);
Uri url = new Uri("https://www.howsmyssl.com/a/check");
HttpResponse response = client.Get(url);

// change settings for the next request
client.Settings.HttpProtocolVersion = "1.1";
client.Settings.TlsClient = new Tls12Client();

response = client.Get(url);

Console.WriteLine(response.Body);
// response:
/*{
  "given_cipher_suites": [
    "TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256",
    "TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256",
    "TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA",
    "TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256",
    "TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256",
    "TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA",
    "TLS_RSA_WITH_AES_128_GCM_SHA256",
    "TLS_RSA_WITH_AES_128_CBC_SHA256",
    "TLS_RSA_WITH_AES_128_CBC_SHA",
    "TLS_EMPTY_RENEGOTIATION_INFO_SCSV"
  ],
  "ephemeral_keys_supported": true,
  "session_ticket_supported": false,
  "tls_compression_supported": false,
  "unknown_cipher_suite_supported": false,
  "beast_vuln": false,
  "able_to_detect_n_minus_one_splitting": false,
  "insecure_cipher_suites": {},
  "tls_version": "TLS 1.2",
  "rating": "Probably Okay"
}*/
```
Please take a look at the unit tests for more dedicated examples. 

## API
namespace `DecentHttpClient`.

### `HttpClient : WebRequest`
Supports basic http methods: `Get, Post, Put, Delete, Patch, Head`.

NOTE #1: at the moment the methods above only support HTTPS requests.
NOTE #2: none of the parent `WebRequest` class methods are implemented/overriden yet.

### `HttpResponse : WebResponse`
#### Properties
`Headers` - response headers in `key:value` pairs
`ProtocolVersion` - response HTTP protocol version
`StatusCode` - response status code
`Body` - response body