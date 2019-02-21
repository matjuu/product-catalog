using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Catalog.API.Contracts.Commands
{
    /// <summary>
    /// Represents a request to create a product
    /// </summary>
    public class UpdateProduct
    {
        /// <summary>
        /// Unique product identifier value
        /// </summary>
        [FromRoute(Name = "id")]
        [JsonIgnore]
        public Guid Id { get; set; }
        /// <summary>
        /// Product code
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        [Required]
        public double Price { get; set; }
    }
}