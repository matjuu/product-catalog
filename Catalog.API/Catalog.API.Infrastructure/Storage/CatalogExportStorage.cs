using System.IO;
using System.Threading.Tasks;

namespace Catalog.API.Infrastructure.Storage
{
    public class CatalogExportStorage : ICatalogExportStorage
    {
        private readonly string _catalogExportsPath;

        public CatalogExportStorage(string catalogExportsPath)
        {
            _catalogExportsPath = catalogExportsPath;
        }

        public async Task Save(string name, byte[] fileBytes)
        {
            Directory.CreateDirectory(_catalogExportsPath);
            File.WriteAllBytes($"{_catalogExportsPath}/{name}", fileBytes);
        }

        public async Task Delete(string name)
        {
            var filePath = $"{_catalogExportsPath}/{name}";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}