using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.Domain.Interfaces.Services;

namespace ProductManagement.Domain.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public void Add(Supplier supplier)
        {
            _supplierRepository.Insert(supplier);
        }
    }
}
