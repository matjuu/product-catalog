using System;

namespace Catalog.API.Domain
{
    public abstract class AggregateRoot<TState> : IAggregateRoot where TState : IAggregateState
    {
        protected TState State { get; set; }

        public Guid Id => State.Id;
    }
}
