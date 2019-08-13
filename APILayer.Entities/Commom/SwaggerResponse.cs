using System;
using System.Collections.Generic;

namespace APILayer.Entities
{
    public class SwaggerResponse
    {
        public string StatusCode { get; private set; }

        public string Body { get; private set; }

        public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public SwaggerResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers)
        {
            StatusCode = statusCode;
            Headers = headers;
        }

        public SwaggerResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers, string body) : this(statusCode, headers)
        {
            this.Body = body;
        }
    }

    public class SwaggerResponse<TResult> : SwaggerResponse
    {
        public TResult Result { get; private set; }

        public SwaggerResponse(string statusCode, IDictionary<string, IEnumerable<string>> headers, TResult result)
            : base(statusCode, headers)
        {
            Result = result;
        }
    }
}
