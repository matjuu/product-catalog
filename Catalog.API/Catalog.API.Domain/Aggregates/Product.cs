using System;
using Catalog.API.Domain.Contracts.Commands;
using Catalog.API.Domain.Contracts.Exceptions;

namespace Catalog.API.Domain.Aggregates
{
    public class Product : AggregateRoot
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool PriceApproved { get; set; }

        private const double PRICE_APPROVAL_BOUNDARY = 999;
        private const double PRICE_LOWER_BOUNDARY = 0;

        public Product(string code, string name, double price)
        {
            var priceApproved = IsPriceAutomaticallyApproved(price);

            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            Price = price;
            PriceApproved = priceApproved;
            LastUpdated = DateTime.UtcNow;

            Validate();
        }

        public void Update(UpdateProduct command)
        {
            Code = command.Code;
            Name = command.Name;
            Price = command.Price;
            PriceApproved = IsPriceAutomaticallyApproved(command.Price);
            LastUpdated = DateTime.UtcNow;

            Validate();
        }

        public void UpdateImage(UpdateProductImage command)
        {
            Image = command.ImageName;
            LastUpdated = DateTime.UtcNow;

            Validate();
        }

        public void ApprovePrice(ApproveProductPrice command)
        {
            if(PriceApproved) throw new PriceAlreadyApproved();

            PriceApproved = command.PriceApproved;
            LastUpdated = DateTime.UtcNow;

            Validate();
        }

        private bool IsPriceAutomaticallyApproved(double amount)
        {
            return amount <= PRICE_APPROVAL_BOUNDARY;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ProductNameEmpty();
            if (string.IsNullOrWhiteSpace(Code)) throw new ProductCodeEmpty();
            if (Price < PRICE_LOWER_BOUNDARY) throw new PriceInvalid(Price, PRICE_LOWER_BOUNDARY);
        }
    }
}
