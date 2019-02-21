using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Contracts.Commands
{
    /// <summary>
    /// Represents a request to delete a catalog export
    /// </summary>
    public class DeleteCatalogExport
    {
        /// <summary>
        /// The identifier for the catalog export
        /// </summary>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}