namespace Catalog.API.Domain.Contracts.Commands
{
    public class ApprovePriceCommand : ICommand
    {
        public ApprovePriceCommand()
        {
            PriceApproved = true;
        }

        public bool PriceApproved { get; }
    }
}