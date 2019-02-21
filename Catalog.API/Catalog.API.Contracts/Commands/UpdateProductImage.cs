using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Contracts.Commands
{
    /// <summary>
    /// Represents a request to upload an image
    /// </summary>
    public class UpdateProductImage
    {
        /// <summary>
        /// The identifier for the product
        /// </summary>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// An object representing an uploaded image file
        /// </summary>
        [FromForm]
        public IFormFile Image { get; set; }
    }
}