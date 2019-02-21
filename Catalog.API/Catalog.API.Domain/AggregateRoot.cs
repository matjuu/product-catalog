using System;

namespace Catalog.API.Domain
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; set; }
    }
}
