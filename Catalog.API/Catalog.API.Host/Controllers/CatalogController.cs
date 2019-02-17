using System;
using System.ComponentModel;
using Catalog.API.Contracts.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Host.Controllers
{
    /// <summary>
    /// A controller for catalog related behaviors such as excel export
    /// </summary>
    [Route("api/catalog/export")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        /// <param name="id">Unique report identifier value</param>
        /// <returns>An object representing an exported Catalog</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CatalogExport), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        /// <param name="id">Unique report identifier value</param>
        /// <returns>An object representing an exported Catalog file</returns>
        [HttpGet("{id}/file")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult GetFile(Guid id)
        {
            return Ok();
        }

        /// <param name="request">An object representing a request to create a Catalog Export</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CatalogExport), 201)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult Create()
        {
            return Created("api/catalog/export/1", null);
        }

        /// <param name="id">Unique product identifier value</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 404)]
        public IActionResult Delete(Guid id)
        {
            return NoContent();
        }
    }
}