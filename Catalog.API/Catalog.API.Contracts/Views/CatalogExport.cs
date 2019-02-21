using System;

namespace Catalog.API.Contracts.Views
{
    /// <summary>
    /// An object representing an exported catalog
    /// </summary>
    public class CatalogExport
    {
        /// <summary>
        /// Unique identifier for the catalog export
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the exported catalog
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Name of the exported catalog file
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The time this was created at
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Status of the document export progress
        /// </summary>
        public ExportStatus Status { get; set; }
    }
}