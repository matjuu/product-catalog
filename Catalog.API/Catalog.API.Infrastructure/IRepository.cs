using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Domain;
using Catalog.API.Domain.Aggregates;

namespace Catalog.API.Infrastructure
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<TAggregate> Get(Guid id);
        Task<TAggregate> Save(TAggregate aggregate);
        Task<TAggregate> Update(TAggregate aggregate);
        Task Delete(TAggregate aggregate);
    }

    public interface IProductsRepository : IRepository<Product>
    {
        Task<Product> GetByCode(string code);
        Task<IEnumerable<Product>> GetAll();
    }
}
