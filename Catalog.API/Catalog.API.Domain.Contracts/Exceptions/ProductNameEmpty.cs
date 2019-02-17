namespace Catalog.API.Domain.Contracts.Exceptions
{
    public class ProductNameEmpty : DomainException
    {
        public ProductNameEmpty() : base("Product name cannot be an empty string.", nameof(ProductNameEmpty))
        {

        }
    }
}