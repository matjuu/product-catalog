using System.Linq;
using System.Threading.Tasks;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Queries;
using Catalog.API.Contracts.Views;
using Catalog.API.Data;
using Catalog.API.Infrastructure;

namespace Catalog.API.ApplicationServices.QueryHandlers
{
    public class QueryProductById : HandlerBase<ProductById, Product>
    {
        private readonly CatalogDbContext _dbContext;

        public QueryProductById(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<Product> HandleCore(ProductById request)
        {
            var result = _dbContext.Products.Where(x => x.Id == request.Id).Select(p => new Product
            {
                Image = p.Image, Code = p.Code, Name = p.Name, Id = p.Id, Price = p.Price,
                PriceApproved = p.PriceApproved, LastUpdated = p.LastUpdated
            }).SingleOrDefault();

            if(result == null) throw new NotFoundException();

            return result;
        }
    }
}