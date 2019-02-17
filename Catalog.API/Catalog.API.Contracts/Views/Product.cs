using System;
using Catalog.API.Contracts.Shared;

namespace Catalog.API.Contracts.Views
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Price Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
