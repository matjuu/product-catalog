using System.Threading.Tasks;
using Catalog.API.Contracts.Requests;
using Catalog.API.Contracts.Views;

namespace Catalog.API.ApplicationServices.RequestHandlers
{
    public class CreateProductRequestHandler : RequestHandlerBase<CreateProductRequest, Product>
    {
        protected override Task<Product> HandleCore(CreateProductRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
