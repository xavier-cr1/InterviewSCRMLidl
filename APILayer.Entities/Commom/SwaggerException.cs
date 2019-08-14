using System;
using System.Collections.Generic;
using System.Text;

namespace APILayer.Entities.Commom
{
    /// <summary>API service in swagger hub. Custom swagger exceptions.</summary>
    /// <seealso cref="System.Exception" />
    public class SwaggerException : Exception
    {
        public string StatusCode { get; private set; }

        public string Response { get; private set; }

        public SwaggerException(string message, string statusCode, string response, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public SwaggerException(string message, System.Exception exception)
            : base(message, exception)
        {
        }
    }
}
