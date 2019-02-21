using System;
using System.Collections.Generic;
using System.Net;

namespace Catalog.API.ApplicationServices.Exceptions
{
    public static class DomainExceptionMappings
    {
        private static readonly Dictionary<Type, HttpStatusCode> Mappings = new Dictionary<Type, HttpStatusCode>();

        public static HttpStatusCode Get(Type exceptionType)
        {
            return Mappings.ContainsKey(exceptionType) ? Mappings[exceptionType] : HttpStatusCode.InternalServerError;
        }

        public static void Register<TTYpe>(HttpStatusCode code)
        {
            Mappings.Add(typeof(TTYpe), code);
        }
    }
}
