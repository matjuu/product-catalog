using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Contracts.Commands
{
    /// <summary>
    /// Represents a request to confirm a product's price
    /// </summary>
    public class ApproveProductPrice
    {
        /// <summary>
        /// The identifier for the product
        /// </summary>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}