namespace Catalog.API.Domain.Contracts.Commands
{
    public class UpdateProductImage : ICommand
    {
        public UpdateProductImage(string imageName)
        {
            ImageName = imageName;
        }

        public string ImageName { get; }
    }
}