namespace Catalog.API.Domain.Contracts.Exceptions
{
    public class PriceAlreadyApproved : DomainException
    {
        public PriceAlreadyApproved() : base("Price is already approved.", nameof(PriceAlreadyApproved))
        {
        }
    }
}