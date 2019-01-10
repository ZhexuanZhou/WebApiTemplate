using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TemplateDemo.Infrastrature.ViewModels;
using TemplateDemo.Infrastrature.ViewModelValidations;

namespace TemplateDemo.Infrastrature.Configurations
{
    public static class ValidationConfig
    {
        public static void AddValidationConfig(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RegistrationViewModel>, RegistrationValidation>();
            services.AddTransient<IValidator<LoginViewModel>, LoginValidation>();
        }
    }
}