namespace Catalog.API.Domain.Contracts.Commands
{
    public class ApproveProductPrice : ICommand
    {
        public ApproveProductPrice()
        {
            PriceApproved = true;
        }

        public bool PriceApproved { get; }
    }
}