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
    /// A controller for catalog related behaviors such as excel export
    /// </summary>
    [Route("api/catalog/exports")]
    [ApiController]
    public class CatalogExportsController : ControllerBase
    {
        private readonly IHandler<CatalogExportsByFilter, IEnumerable<CatalogExport>> _queryCatalogExportsByFilter;

        private readonly IHandler<CreateCatalogExport, CatalogExport> _whenCreateCatalogExport;
        private readonly IHandler<DeleteCatalogExport> _whenDeleteCatalogExport;


        /// <inheritdoc />
        public CatalogExportsController(
            IHandler<CatalogExportsByFilter, IEnumerable<CatalogExport>> queryCatalogExportsByFilter,
            IHandler<CreateCatalogExport, CatalogExport> whenCreateCatalogExport,
            IHandler<DeleteCatalogExport> whenDeleteCatalogExport)
        {
            _queryCatalogExportsByFilter = queryCatalogExportsByFilter;
            _whenCreateCatalogExport = whenCreateCatalogExport;
            _whenDeleteCatalogExport = whenDeleteCatalogExport;
        }

        /// <param name="request">Represents a request to get a list of catalog exports</param>
        /// <returns>An object representing an exported Catalog</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogExport>), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> Get([FromQuery] CatalogExportsByFilter request)
        {
            var result = await _queryCatalogExportsByFilter.Handle(request);
            return Ok(result);
        }

        /// <param name="request">An object representing a request to create a Catalog Export</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CatalogExport) ,201)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Create(CreateCatalogExport request)
        {
            var result = await _whenCreateCatalogExport.Handle(request);
            return Created($"api/catalog/export/{result.Id}", result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Error), 404)]
        public async Task<IActionResult> Delete([FromRoute] DeleteCatalogExport request)
        {
            await _whenDeleteCatalogExport.Handle(request);
            return NoContent();
        }
    }
}