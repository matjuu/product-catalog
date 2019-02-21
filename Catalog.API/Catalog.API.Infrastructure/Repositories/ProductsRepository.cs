using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Domain.Aggregates;

namespace Catalog.API.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductsRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Get(Guid id)
        {
            return _dbContext.Products.SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return  _dbContext.Products.ToList();
        }

        public async Task<Product> GetByCode(string code)
        {
            return _dbContext.Products.SingleOrDefault(p => p.Code == code);
        }

        public async Task<Product> Save(Product aggregate)
        {
            var product = _dbContext.Products.Add(aggregate);
            await _dbContext.SaveChangesAsync();

            return product.Entity;
        }

        public async Task<Product> Update(Product aggregate)
        {
            var product = _dbContext.Products.Update(aggregate);
            await _dbContext.SaveChangesAsync();

            return product.Entity;
        }

        public async Task Delete(Product aggregate)
        {
            _dbContext.Products.Remove(aggregate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
