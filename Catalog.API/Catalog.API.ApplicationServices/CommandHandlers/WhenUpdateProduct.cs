using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Commands;
using Catalog.API.Contracts.Views;
using Catalog.API.Infrastructure;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenUpdateProduct : HandlerBase<UpdateProduct, Product>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public WhenUpdateProduct(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        protected override async Task<Product> HandleCore(UpdateProduct request)
        {
            var aggregate = await _productsRepository.Get(request.Id);
            if (aggregate == null) throw new NotFoundException();

            await ValidateAggregateCodeUniqueness(request, aggregate);

            var command = _mapper.Map<Domain.Contracts.Commands.UpdateProduct>(request);
            aggregate.Update(command);

            var result = await _productsRepository.Update(aggregate);
            return _mapper.Map<Product>(result);
        }

        private async Task ValidateAggregateCodeUniqueness(UpdateProduct request, Domain.Aggregates.Product aggregate)
        {
            if (aggregate.Code != request.Code)
            {
                var product = await _productsRepository.GetByCode(request.Code);
                if (product != null)
                    throw new ApiException("A product with the provided code already exists. Codes must be unique.",
                        "ProductCodeTaken", HttpStatusCode.BadRequest);
            }
        }
    }
}