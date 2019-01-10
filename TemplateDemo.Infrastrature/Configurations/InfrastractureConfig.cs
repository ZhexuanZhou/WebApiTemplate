using Microsoft.Extensions.DependencyInjection;
using TemplateDemo.Core.Interfaces.RepositoryInterfaces;
using TemplateDemo.Infrastrature.Repositories;

namespace TemplateDemo.Infrastrature.Configurations
{
    public static class InfrastractureConfig
    {
        public static void AddInfrastractureConfig(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}