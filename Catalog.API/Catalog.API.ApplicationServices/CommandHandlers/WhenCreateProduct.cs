using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Commands;
using Catalog.API.Contracts.Views;
using Catalog.API.Infrastructure;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenCreateProduct : HandlerBase<CreateProduct, Product>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public WhenCreateProduct(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        protected override async Task<Product> HandleCore(CreateProduct request)
        {
            await ValidateProductCodeUniqueness(request);

            var aggregate = new Domain.Aggregates.Product(request.Code, request.Name, request.Price);

            await _productsRepository.Save(aggregate);
            return _mapper.Map<Domain.Aggregates.Product, Product>(aggregate);
        }

        private async Task ValidateProductCodeUniqueness(CreateProduct request)
        {
            var aggregate = await _productsRepository.GetByCode(request.Code);
            if (aggregate != null)
                throw new ApiException("A product with the provided code already exists. Codes must be unique.",
                    "ProductCodeTaken", HttpStatusCode.BadRequest);
        }
    }
}
