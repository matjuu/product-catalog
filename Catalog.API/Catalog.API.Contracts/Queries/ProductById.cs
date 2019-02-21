using System;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Contracts.Queries
{
    public class ProductById
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
