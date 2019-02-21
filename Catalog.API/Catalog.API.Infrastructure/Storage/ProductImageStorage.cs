using System.IO;
using System.Threading.Tasks;

namespace Catalog.API.Infrastructure.Storage
{
    public class ProductImageStorage : IProductImageStorage
    {
        private readonly string _productImagesPath;

        public ProductImageStorage(string productImagesPath)
        {
            _productImagesPath = productImagesPath;
        }

        public async Task Save(string name, byte[] fileBytes)
        {
            Directory.CreateDirectory(_productImagesPath);
            File.WriteAllBytes($"{_productImagesPath}/{name}", fileBytes);
        }

        public async Task Delete(string name)
        {
            var filePath = $"{_productImagesPath}/{name}";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}