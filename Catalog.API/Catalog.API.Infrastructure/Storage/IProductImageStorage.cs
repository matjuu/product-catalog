using System.Threading.Tasks;

namespace Catalog.API.Infrastructure.Storage
{
    public interface IProductImageStorage
    {
        Task Save(string name, byte[] fileBytes);
        Task Delete(string name);
    }
}