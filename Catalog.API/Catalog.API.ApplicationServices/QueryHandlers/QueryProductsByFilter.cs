using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Contracts.Queries;
using Catalog.API.Contracts.Views;
using Catalog.API.Data;
using Catalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.ApplicationServices.QueryHandlers
{
    public class QueryProductsByFilter : HandlerBase<ProductsByFilter, IEnumerable<Product>>
    {
        private readonly CatalogDbContext _dbContext;

        public QueryProductsByFilter(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IEnumerable<Product>> HandleCore(ProductsByFilter request)
        {
            var query = BuildQuery(request);

            var result = query.Select(p => new Product
            {
                Image = p.Image,
                Code = p.Code,
                Name = p.Name,
                Id = p.Id,
                Price = p.Price,
                PriceApproved = p.PriceApproved,
                LastUpdated = p.LastUpdated
            }).ToList();

            return result;
        }

        private IQueryable<Domain.Aggregates.Product> BuildQuery(ProductsByFilter request)
        {
            var query = _dbContext.Products.AsNoTracking()
                .Skip(request.Offset)
                .Take(request.Limit);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Code.Contains(request.Search) || x.Name.Contains(request.Search));
            }

            return query;
        }
    }
}