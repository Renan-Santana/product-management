using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
    }
}
