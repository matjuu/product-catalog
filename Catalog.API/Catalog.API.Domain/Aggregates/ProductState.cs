using System;

namespace Catalog.API.Domain.Aggregates
{
    public class ProductState : IAggregateState
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public bool PriceApproved { get; set; }
    }
}