namespace Catalog.API.Domain.Contracts.Exceptions
{
    public class CatalogExportAlreadyCompleted : DomainException
    {
        public CatalogExportAlreadyCompleted() : base("Catalog export already completed", nameof(CatalogExportAlreadyCompleted))
        {
        }
    }
}