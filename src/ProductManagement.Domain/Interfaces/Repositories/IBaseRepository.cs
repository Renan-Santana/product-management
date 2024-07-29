using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TEntityId> where TEntity : EntityBase<TEntityId>
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(TEntityId id);
        (IEnumerable<TEntity> pageRecords, int totalRecords) GetByFilter(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        (IEnumerable<TEntity> pageRecords, int totalRecords) GetAll(int pageNumber, int pageSize);
    }
}
