using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.ApplicationServices.Excel;
using Catalog.API.Contracts.Commands;
using Catalog.API.Contracts.Views;
using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Storage;
using ExportStatus = Catalog.API.Domain.ExportStatus;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenCreateCatalogExport : HandlerBase<CreateCatalogExport, CatalogExport>
    {
        private readonly ICatalogExportFileProducer _catalogExportFileProducer;
        private readonly ICatalogExportStorage _catalogExportStorage;
        private readonly IProductsRepository _productsRepository;
        private readonly IRepository<Domain.Aggregates.CatalogExport> _catalogExportsRepository;
        private readonly IMapper _mapper;

        public WhenCreateCatalogExport(ICatalogExportFileProducer catalogExportFileProducer,
            ICatalogExportStorage catalogExportStorage,
            IProductsRepository productsRepository,
            IRepository<Domain.Aggregates.CatalogExport> catalogExportsRepository, 
            IMapper mapper)
        {
            _catalogExportFileProducer = catalogExportFileProducer;
            _catalogExportStorage = catalogExportStorage;
            _productsRepository = productsRepository;
            _catalogExportsRepository = catalogExportsRepository;
            _mapper = mapper;
        }

        protected override async Task<CatalogExport> HandleCore(CreateCatalogExport request)
        {

            var aggregate = new Domain.Aggregates.CatalogExport
            {
                Id = Guid.NewGuid(),
                Name = $"Export {DateTime.UtcNow}",
                Status = ExportStatus.InProgress,
                CreatedAt = DateTime.UtcNow,
                FileName = $"Export {Guid.NewGuid()}.xlsx"
            };

            await _catalogExportsRepository.Save(aggregate);

            //TODO introduce a persistent messaging mechanism since this might be a long running process
            var products = await _productsRepository.GetAll();
            var excelFileBytes = await _catalogExportFileProducer.Produce(products.ToList());
            await _catalogExportStorage.Save(aggregate.FileName, excelFileBytes);

            aggregate.MarkExportCompleted();

            await _catalogExportsRepository.Update(aggregate);
            return _mapper.Map<CatalogExport>(aggregate);
        }
    }
}