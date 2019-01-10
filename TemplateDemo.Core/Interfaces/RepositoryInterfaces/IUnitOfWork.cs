using System.Threading.Tasks;

namespace TemplateDemo.Core.Interfaces.RepositoryInterfaces
{
    public interface IUnitOfWork
    {
         Task<bool> SaveChangsAsync();
    }
}