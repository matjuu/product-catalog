using System.Threading.Tasks;
using Catalog.API.Contracts.Requests;
using Catalog.API.Contracts.Views;

namespace Catalog.API.ApplicationServices.RequestHandlers
{
    public class UpdateProductRequestHandler : RequestHandlerBase<UpdateProductRequest, Product>
    {
        protected override Task<Product> HandleCore(UpdateProductRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}