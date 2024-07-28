using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;

namespace ProductManagement.Infrastructure.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier, short>, ISupplierRepository
    {
        public SupplierRepository(ProductManagementContext context) : base(context) { }
    }
}
