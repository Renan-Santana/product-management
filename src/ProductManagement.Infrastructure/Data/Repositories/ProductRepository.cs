using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;

namespace ProductManagement.Infrastructure.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ProductManagementContext context) : base(context) { }

        public Product GetByIdIncluding(int id)
        {
            return _context.Set<Product>().Include(s => s.Supplier).Where(p => p.Id == id).FirstOrDefault();
        }
        public (IEnumerable<Product> pageRecords, int totalRecords) GetAllIncluding(int pageNumber, int pageSize)
        {
            var query = _context.Set<Product>();

            var totalRecords = query.Count();
            var pageRecords = query
                                .Include(s => s.Supplier)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return (pageRecords, totalRecords);
        }

        public (IEnumerable<Product> pageRecords, int totalRecords) GetByFilterIncluding(Expression<Func<Product, bool>> predicate, int pageNumber, int pageSize)
        {
            var query = _context.Set<Product>().Where(predicate);

            var totalRecords = query.Count();
            var pageRecords = query
                                .Include(s => s.Supplier)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return (pageRecords, totalRecords);
        }

        public void SoftDelete(Product entity)
        {
            entity.Situation = false;
            _context.SaveChanges();
        }
    }
}
