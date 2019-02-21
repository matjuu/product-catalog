using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Views;
using Catalog.API.Infrastructure;
using ApproveProductPrice = Catalog.API.Domain.Contracts.Commands.ApproveProductPrice;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenApproveProductPrice : HandlerBase<Contracts.Commands.ApproveProductPrice, Product>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public WhenApproveProductPrice(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        protected override async Task<Product> HandleCore(Contracts.Commands.ApproveProductPrice request)
        {
            var aggregate = await _productsRepository.Get(request.Id);
            if(aggregate == null) throw new NotFoundException();

            aggregate.ApprovePrice(new ApproveProductPrice());

            await _productsRepository.Update(aggregate);

            return _mapper.Map<Product>(aggregate);
        }
    }
}