using System.Net;
using Catalog.API.Domain.Contracts.Exceptions;

namespace Catalog.API.ApplicationServices.Exceptions
{
    public static class DomainExceptionMappingConfiguration
    {
        public static void Configure()
        {
            DomainExceptionMappings.Register<PriceAlreadyApproved>(HttpStatusCode.BadRequest);
            DomainExceptionMappings.Register<PriceInvalid>(HttpStatusCode.BadRequest);
            DomainExceptionMappings.Register<ProductNameEmpty>(HttpStatusCode.BadRequest);
            DomainExceptionMappings.Register<ProductCodeEmpty>(HttpStatusCode.BadRequest);
        }
    }
}