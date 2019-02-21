using System.Net;

namespace Catalog.API.ApplicationServices.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException() : base("Entity not found.", "EntityNotFound", HttpStatusCode.NotFound)
        {
        }
    }
}