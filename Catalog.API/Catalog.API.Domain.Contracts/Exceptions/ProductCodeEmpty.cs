namespace Catalog.API.Domain.Contracts.Exceptions
{
    public class ProductCodeEmpty : DomainException
    {
        public ProductCodeEmpty() : base("Product code cannot be an empty string.", nameof(ProductCodeEmpty))
        {

        }
    }
}