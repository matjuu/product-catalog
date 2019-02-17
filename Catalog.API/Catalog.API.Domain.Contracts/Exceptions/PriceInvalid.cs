namespace Catalog.API.Domain.Contracts.Exceptions
{
    public class PriceInvalid : DomainException
    {
        public PriceInvalid(double price, double lowerBoundary) : base($"Price too low. Price must be higher than {lowerBoundary}.", nameof(PriceInvalid))
        {
            Properties.Add(nameof(price), price.ToString());
            Properties.Add(nameof(lowerBoundary), lowerBoundary.ToString());
        }
    }
}