using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;

namespace ProductManagement.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity, TEntityId> : IBaseRepository<TEntity, TEntityId> where TEntity : EntityBase<TEntityId>
    {
        protected readonly ProductManagementContext _context;

        public BaseRepository(ProductManagementContext context)
        {
            _context = context;
        }

        public void Insert(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TEntity GetById(TEntityId id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public (IEnumerable<TEntity> pageRecords, int totalRecords) GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Set<TEntity>();

            var totalRecords = query.Count();
            var pageRecords = query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return (pageRecords, totalRecords);
        }

        public (IEnumerable<TEntity> pageRecords, int totalRecords) GetByFilter(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            var query = _context.Set<TEntity>().Where(predicate);

            var totalRecords = query.Count();
            var pageRecords = query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return (pageRecords, totalRecords);
        }
    }
}
