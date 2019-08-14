using System;
using System.Collections.Generic;

namespace APILayer.Entities
{
    /// <summary>API service in swagger hub. Generic swagger response.</summary>
    public class SwaggerResponse
    {
        public string StatusCode { get; private set; }

        public string Body { get; private set; }

        public SwaggerResponse(string statusCode)
        {
            StatusCode = statusCode;
        }

        public SwaggerResponse(string statusCode, string body) : this(statusCode)
        {
            this.Body = body;
        }
    }

    /// <summary>API service in swagger hub. Strongly typed swagger response.</summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class SwaggerResponse<TResult> : SwaggerResponse
    {
        public TResult Result { get; private set; }

        public SwaggerResponse(string statusCode, TResult result)
            : base(statusCode)
        {
            Result = result;
        }
    }
}
