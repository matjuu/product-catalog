using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.ApplicationServices;
using Catalog.API.Contracts.Requests;
using Catalog.API.Contracts.Views;
using Microsoft.AspNetCore.Http;
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
        private readonly IRequestHandler<CreateProductRequest, Product> _whenCreateProductRequest;
        private readonly IRequestHandler<UpdateProductRequest, Product> _whenUpdateProductRequest;

        public ProductsController(IRequestHandler<CreateProductRequest, Product> whenCreateProductRequest,
            IRequestHandler<UpdateProductRequest, Product> whenUpdateProductRequest)
        {
            _whenCreateProductRequest = whenCreateProductRequest;
            _whenUpdateProductRequest = whenUpdateProductRequest;
        }

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
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        /// <param name="id">Unique product identifier value</param>
        /// <returns>An object representing a Product Image</returns>
        [HttpGet("{id}/image")]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult GetImage(Guid id)
        {
            return Ok();
        }

        /// <param name="request">An object representing a request to create a Product</param>
        /// <returns>An object representing a Product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var responseObject = await _whenCreateProductRequest.Handle(request);
            return Ok(responseObject);
        }


        /// <param name="request">An object representing a request to create a Catalog Export</param>
        /// <param name="id">Unique product identifier value</param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPut("{id}/image")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult UploadImage(Guid id, [FromBody] IFormFile image)
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var response = await _whenUpdateProductRequest.Handle(request);
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
