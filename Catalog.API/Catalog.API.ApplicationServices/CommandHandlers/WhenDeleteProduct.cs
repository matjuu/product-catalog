using System.Threading.Tasks;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Commands;
using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Storage;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenDeleteProduct : HandlerBase<DeleteProduct>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IProductImageStorage _productImageStorage;

        public WhenDeleteProduct(IProductsRepository productsRepository, IProductImageStorage productImageStorage)
        {
            _productsRepository = productsRepository;
            _productImageStorage = productImageStorage;
        }

        protected override async Task HandleCore(DeleteProduct request)
        {
            var aggregate = await _productsRepository.Get(request.Id);
            if (aggregate == null) throw new NotFoundException();

            await _productsRepository.Delete(aggregate);
            await _productImageStorage.Delete(aggregate.Image);
        }
    }
}