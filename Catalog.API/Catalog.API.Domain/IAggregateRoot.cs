using System;

namespace Catalog.API.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}