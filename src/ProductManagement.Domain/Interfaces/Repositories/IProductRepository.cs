using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
        void SoftDelete(Product entity);
        Product GetByIdIncluding(int id);
        (IEnumerable<Product> pageRecords, int totalRecords) GetAllIncluding(int pageNumber, int pageSize);
        (IEnumerable<Product> pageRecords, int totalRecords) GetByFilterIncluding(Expression<Func<Product, bool>> predicate, int pageNumber, int pageSize);
    }
}
