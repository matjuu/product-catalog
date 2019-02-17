using System.Collections.Generic;
using Catalog.API.Contracts.Requests;
using Catalog.API.Contracts.Views;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Host.Controllers
{
    /// <summary>
    /// A controller for managing and retrieving products
    /// </summary>
    [Route("api/catalog/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <returns>A list of products that match the search criteria.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult Get()
        {
            return Ok();
        }

        /// <param name="id">Unique product identifier value</param>
        /// <returns>An object representing a Product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        /// <param name="id">Unique product identifier value</param>
        /// <returns>An object representing a Product Image</returns>
        [HttpGet("{id}/image")]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult GetImage(int id)
        {
            return Ok();
        }

        /// <param name="request">An object representing a request to create a Product</param>
        /// <returns>An object representing a Product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult Post([FromBody] CreateProductRequest request)
        {
            return Ok();
        }

        /// <param name="id">Unique product identifier value</param>
        /// <param name="request">An object representing a request to update a Product</param>
        /// <returns>An object representing a Product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult Update(int id, [FromBody] UpdateProductRequest request)
        {
            return Ok();
        }

        [HttpPut("{id}/price/confirm")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult ConfirmPrice(int id)
        {
            return Ok();
        }

        /// <param name="id">Unique product identifier value</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
