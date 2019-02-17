using System;

namespace Catalog.API.Domain
{
    public interface IAggregateState
    {
        Guid Id { get; set; }
    }
}