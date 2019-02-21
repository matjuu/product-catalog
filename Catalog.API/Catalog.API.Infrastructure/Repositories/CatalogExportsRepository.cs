using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Domain.Aggregates;

namespace Catalog.API.Infrastructure.Repositories
{
    public class CatalogExportsRepository : IRepository<CatalogExport>
    {
        private readonly CatalogDbContext _dbContext;

        public CatalogExportsRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CatalogExport> Get(Guid id)
        {
            return _dbContext.CatalogExports.SingleOrDefault(ce => ce.Id == id);
        }

        public async Task<CatalogExport> Save(CatalogExport aggregate)
        {
            var product = _dbContext.CatalogExports.Add(aggregate);
            await _dbContext.SaveChangesAsync();

            return product.Entity;
        }

        public async Task<CatalogExport> Update(CatalogExport aggregate)
        {
            var product = _dbContext.CatalogExports.Update(aggregate);
            await _dbContext.SaveChangesAsync();

            return product.Entity;
        }

        public async Task Delete(CatalogExport aggregate)
        {
            _dbContext.CatalogExports.Remove(aggregate);
            await _dbContext.SaveChangesAsync();
        }
    }
}