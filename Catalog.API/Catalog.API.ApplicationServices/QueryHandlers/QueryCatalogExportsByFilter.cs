using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Contracts.Queries;
using Catalog.API.Contracts.Views;
using Catalog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.ApplicationServices.QueryHandlers
{
    public class QueryCatalogExportsByFilter : HandlerBase<CatalogExportsByFilter, IEnumerable<CatalogExport>>
    {
        private readonly CatalogDbContext _dbContext;

        public QueryCatalogExportsByFilter(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IEnumerable<CatalogExport>> HandleCore(CatalogExportsByFilter request)
        {
            var results = _dbContext.CatalogExports.AsNoTracking()
                .Skip(request.Offset)
                .Take(request.Limit)
                .Select(ce => new CatalogExport
                {
                    Name = ce.Name, Id = ce.Id, Status = (ExportStatus) ce.Status, CreatedAt = ce.CreatedAt,
                    FileName = ce.FileName
                }).ToList();

            return results;
        }
    }
}