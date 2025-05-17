
using System.Net;

namespace ShoppEcommerce_WebApp.Common.Exceptions
{
    public class ErrorCodeMetadata
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public ErrorCodeMetadata(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
