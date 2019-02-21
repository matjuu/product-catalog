using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Domain.Aggregates;

namespace Catalog.API.ApplicationServices.Excel
{
    public interface ICatalogExportFileProducer
    {
        Task<byte[]> Produce(List<Product> products);
    }
}
