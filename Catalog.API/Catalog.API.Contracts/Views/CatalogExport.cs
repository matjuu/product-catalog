using System;

namespace Catalog.API.Contracts.Views
{
    public class CatalogExport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}