using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TemplateDemo.Core.Entities;
using TemplateDemo.Core.Interfaces.RepositoryInterfaces;
using TemplateDemo.Infrastrature.Database;

namespace TemplateDemo.Infrastrature.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity, Guid> 
        where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RepositoryBase(ApplicationDbContext applicationDbContext) 
            => _applicationDbContext = applicationDbContext;

        public void Add(TEntity entity) 
            => _applicationDbContext.Set<TEntity>().Add(entity);

        public async Task AddAsync(TEntity entity) 
            => await _applicationDbContext.Set<TEntity>().AddAsync(entity);

        public void AddRange(List<TEntity> entities) 
            => _applicationDbContext.Set<TEntity>().AddRange(entities);

        public async Task AddRangeAsync(List<TEntity> entities) 
            => await _applicationDbContext.Set<TEntity>().AddRangeAsync(entities);

        public void Delete(TEntity entity) 
            => _applicationDbContext.Set<TEntity>().Remove(entity);

        public IQueryable<TEntity> FindAll() 
            => _applicationDbContext.Set<TEntity>().AsQueryable();

        public async Task<IQueryable<TEntity>> FindAllAysnc() 
            => (await _applicationDbContext.Set<TEntity>().ToListAsync()).AsQueryable();

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate) 
            => _applicationDbContext.Set<TEntity>().Where(predicate).AsQueryable();

        public async Task<IQueryable<TEntity>> FindByConditionAsync(
            Expression<Func<TEntity, bool>> predicate) 
            => (await _applicationDbContext.Set<TEntity>()
                .Where(predicate).ToListAsync()).AsQueryable();

        public TEntity FindById(Guid id) 
            => _applicationDbContext.Set<TEntity>().Where(e => e.Id == id).FirstOrDefault();

        public async Task<TEntity> FindByIdAysnc(Guid id) 
            => await _applicationDbContext.Set<TEntity>()
                .Where(e => e.Id == id).FirstOrDefaultAsync();

        public void Update(TEntity entity)
        {
            var obj = _applicationDbContext.Set<TEntity>().Where(e=>e.Id == entity.Id).FirstOrDefault();
            _applicationDbContext.Entry(obj).CurrentValues.SetValues(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var obj = await _applicationDbContext.Set<TEntity>().Where(e=>e.Id == entity.Id).FirstOrDefaultAsync();
            _applicationDbContext.Entry(obj).CurrentValues.SetValues(entity);
        }
    }
}