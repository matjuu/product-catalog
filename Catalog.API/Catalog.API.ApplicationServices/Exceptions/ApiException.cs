using System;
using System.Collections.Generic;
using System.Net;

namespace Catalog.API.ApplicationServices.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, string code, HttpStatusCode statusCode, IDictionary<string, string> properties = null) : base(message)
        {
            Code = code;
            Properties = properties;
            StatusCode = statusCode;
        }

        public string Code { get; }
        public HttpStatusCode StatusCode { get; set; }
        public IDictionary<string, string> Properties { get; }
    }
}
