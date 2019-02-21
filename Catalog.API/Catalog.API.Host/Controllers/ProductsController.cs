using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.ApplicationServices;
using Catalog.API.Contracts.Commands;
using Catalog.API.Contracts.Queries;
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
        private readonly IHandler<ProductsByFilter, IEnumerable<Product>> _queryProductsByFilter;
        private readonly IHandler<ProductById, Product> _queryProductById;

        private readonly IHandler<CreateProduct, Product> _whenCreateProduct;
        private readonly IHandler<UpdateProduct, Product> _whenUpdateProduct;
        private readonly IHandler<UpdateProductImage, Product> _whenUpdateProductImage;
        private readonly IHandler<ApproveProductPrice, Product> _whenApproveProductPrice;
        private readonly IHandler<DeleteProduct> _whenDeleteProduct;

        /// <inheritdoc />
        public ProductsController(IHandler<CreateProduct, Product> whenCreateProduct,
            IHandler<UpdateProduct, Product> whenUpdateProduct,
            IHandler<ProductsByFilter, IEnumerable<Product>> queryProductsByFilter,
            IHandler<ProductById, Product> queryProductById, IHandler<UpdateProductImage, Product> whenUpdateProductImage, IHandler<ApproveProductPrice, Product> whenApproveProductPrice, IHandler<DeleteProduct> whenDeleteProduct)
        {
            _whenCreateProduct = whenCreateProduct;
            _whenUpdateProduct = whenUpdateProduct;
            _queryProductsByFilter = queryProductsByFilter;
            _queryProductById = queryProductById;
            _whenUpdateProductImage = whenUpdateProductImage;
            _whenApproveProductPrice = whenApproveProductPrice;
            _whenDeleteProduct = whenDeleteProduct;
        }

        /// <param name="query">An object describing the query parameters</param>
        /// <returns>A list of products that match the search criteria.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get([FromQuery] ProductsByFilter query)
        {
            var result = await _queryProductsByFilter.Handle(query);
            return Ok(result);
        }

        /// <param name="query">An object describing the query parameters</param>
        /// <returns>An object representing a Product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> Get([FromQuery] ProductById query)
        {
            var result = await _queryProductById.Handle(query);
            return Ok(result);
        }

        /// <param name="command">An object representing a request to create a Product</param>
        /// <returns>An object representing a Product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Create([FromBody] CreateProduct command)
        {
            var result = await _whenCreateProduct.Handle(command);
            return Ok(result);
        }

        /// <param name="updateProductImage">An object representing a request to update a product's image</param>
        /// <returns></returns>
        [HttpPut("{id}/image")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateProductImage updateProductImage)
        {
            var result = await _whenUpdateProductImage.Handle(updateProductImage);
            return Ok(result);
        }

        /// <param name="id">Product id</param>
        /// <param name="request">An object representing a request to update a Product</param>
        /// <returns>An object representing a Product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProduct request)
        {
            request.Id = id;
            var response = await _whenUpdateProduct.Handle(request);
            return Ok(response);
        }

        /// <summary>
        /// An endpoint for confirming a price if it needs it
        /// </summary>
        /// <param name="request">Represents a request to approve product price</param>
        /// <returns></returns>
        [HttpPut("{id}/price/approve")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> ConfirmPrice([FromRoute] ApproveProductPrice request)
        {
            var result = await _whenApproveProductPrice.Handle(request);
            return Ok(result);
        }

        /// <param name="request">A request to delete a product</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> Delete([FromQuery] DeleteProduct request)
        {
            await _whenDeleteProduct.Handle(request);
            return NoContent();
        }
    }
}
