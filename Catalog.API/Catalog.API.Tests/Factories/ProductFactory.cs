using Catalog.API.Domain.Aggregates;

namespace Catalog.API.Tests.Factories
{
    public static class ProductFactory
    {
        public static Product Create(string code = "code", string name = "name", double price = 100)
        {
            return new Product(code, name, price);
        }
    }
}
