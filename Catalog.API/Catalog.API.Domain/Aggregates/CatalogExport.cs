using System;
using Catalog.API.Domain.Contracts.Exceptions;

namespace Catalog.API.Domain.Aggregates
{
    public class CatalogExport : AggregateRoot
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ExportStatus Status { get; set; }

        public void MarkExportCompleted()
        {
            if (Status == ExportStatus.Completed) throw new CatalogExportAlreadyCompleted();
            Status = ExportStatus.Completed;
        }
    }
}
