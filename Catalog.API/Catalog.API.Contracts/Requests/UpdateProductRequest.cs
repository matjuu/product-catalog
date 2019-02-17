using Catalog.API.Contracts.Shared;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Contracts.Requests
{
    public class UpdateProductRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public Price Price { get; set; }
    }
}