using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateDemo.Core.Entities;

namespace TemplateDemo.Core.Interfaces.RepositoryInterfaces
{
    public interface IRepositoryBase<TEntity, TId> where TEntity : EntityBase
    {
        // Create
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);

        // Read
        TEntity FindById(TId id);
        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByIdAysnc(TId id);
        Task<IQueryable<TEntity>> FindAllAysnc();
        Task<IQueryable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);

        // Update
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);

        // Delete
        void Delete(TEntity entity);
    }
}