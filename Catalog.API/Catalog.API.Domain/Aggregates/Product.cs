using System;
using Catalog.API.Domain.Contracts.Commands;
using Catalog.API.Domain.Contracts.Exceptions;

namespace Catalog.API.Domain.Aggregates
{
    public class Product : AggregateRoot<ProductState>
    {
        private const double PRICE_APPROVAL_BOUNDARY = 999;
        private const double PRICE_LOWER_BOUNDARY = 0;

        public Product(ProductState state)
        {
            State = state;

            Validate();
        }

        public Product(string code, string name, double price)
        {
            var priceApproved = IsPriceAutomaticallyApproved(price);

            State = new ProductState
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Price = price,
                PriceApproved = priceApproved
            };

            Validate();
        }

        public void Update(UpdateProductCommand command)
        {
            State.Code = command.Code;
            State.Name = command.Name;
            State.Price = command.Price;
            State.PriceApproved = IsPriceAutomaticallyApproved(command.Price);

            Validate();
        }

        public void ApprovePrice(ApprovePriceCommand command)
        {
            if(State.PriceApproved) throw new PriceAlreadyApproved();

            State.PriceApproved = command.PriceApproved;
        }

        private bool IsPriceAutomaticallyApproved(double amount)
        {
            return amount <= PRICE_APPROVAL_BOUNDARY;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(State.Name)) throw new ProductNameEmpty();
            if (string.IsNullOrWhiteSpace(State.Code)) throw new ProductCodeEmpty();
            if (State.Price < PRICE_LOWER_BOUNDARY) throw new PriceInvalid(State.Price, PRICE_LOWER_BOUNDARY);
        }
    }
}
