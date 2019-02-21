using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Contracts.Commands
{
    /// <summary>
    /// Represents a request to create a product
    /// </summary>
    public class CreateProduct
    {
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
