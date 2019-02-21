using System;

namespace Catalog.API.Contracts.Views
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identifier for the product
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Product code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product image file name
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Product price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Last update date
        /// </summary>
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Indicates whether the price is approved or not
        /// </summary>
        public bool PriceApproved { get; set; }
    }
}
