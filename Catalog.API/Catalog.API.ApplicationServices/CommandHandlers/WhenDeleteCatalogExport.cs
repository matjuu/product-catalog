using System.Threading.Tasks;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Commands;
using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Storage;
using CatalogExport = Catalog.API.Domain.Aggregates.CatalogExport;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenDeleteCatalogExport : HandlerBase<DeleteCatalogExport>
    {
        private readonly IRepository<CatalogExport> _catalogExportsRepository;
        private readonly ICatalogExportStorage _catalogExportStorage;

        public WhenDeleteCatalogExport(ICatalogExportStorage catalogExportStorage, IRepository<CatalogExport> catalogExportsRepository)
        {
            _catalogExportStorage = catalogExportStorage;
            _catalogExportsRepository = catalogExportsRepository;
        }

        protected override async Task HandleCore(DeleteCatalogExport request)
        {
            var aggregate = await _catalogExportsRepository.Get(request.Id);
            if(aggregate == null) throw new NotFoundException();

            await _catalogExportsRepository.Delete(aggregate);
            await _catalogExportStorage.Delete(aggregate.FileName);
        }
    }
}