using System;
using System.Collections.Generic;

namespace Catalog.API.Domain.Contracts
{
    public class DomainException : Exception
    {
        public DomainException(string message, string code, IDictionary<string, string> properties = null) : base(message)
        {
            Code = code;
            Properties = properties;
        }

        public string Code { get; }
        public IDictionary<string,string> Properties { get; }
    }
}