namespace Catalog.API.Domain.Contracts.Commands
{
    public class UpdateProductCommand : ICommand
    {
        public UpdateProductCommand(string code, string name, string image, double price)
        {
            Code = code;
            Name = name;
            Image = image;
            Price = price;
        }

        public string Code { get;  }
        public string Name { get; }
        public string Image { get; }
        public double Price { get; }
    }
}
