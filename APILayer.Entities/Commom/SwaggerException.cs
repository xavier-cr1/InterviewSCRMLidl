using System;
using System.Collections.Generic;
using System.Text;

namespace APILayer.Entities.Commom
{
    public class SwaggerException : Exception
    {
        public string StatusCode { get; private set; }

        public string Response { get; private set; }

        public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public SwaggerException(string message, string statusCode, string response, IDictionary<string, IEnumerable<string>> headers, System.Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public SwaggerException(string message, System.Exception exception)
            : base(message, exception)
        {
        }
    }
}
