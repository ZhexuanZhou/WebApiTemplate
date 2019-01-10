using System.Threading.Tasks;
using TemplateDemo.Core.Interfaces.RepositoryInterfaces;
using TemplateDemo.Infrastrature.Database;

namespace TemplateDemo.Infrastrature.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext) 
            => _applicationDbContext = applicationDbContext;
        public async Task<bool> SaveChangsAsync() 
            => await _applicationDbContext.SaveChangesAsync() > 0;
    }
}