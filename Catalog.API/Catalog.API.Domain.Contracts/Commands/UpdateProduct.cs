namespace Catalog.API.Domain.Contracts.Commands
{
    public class UpdateProduct : ICommand
    {
        public UpdateProduct(string code, string name, double price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public string Code { get;  }
        public string Name { get; }
        public double Price { get; }
    }
}
