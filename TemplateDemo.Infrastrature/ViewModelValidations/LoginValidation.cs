using FluentValidation;
using TemplateDemo.Infrastrature.ViewModels;

namespace TemplateDemo.Infrastrature.ViewModelValidations
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        public LoginValidation()
        {
            RuleFor(x=>x.Email).NotNull().WithMessage("Email cannot be null");
            RuleFor(x=>x.Password).NotNull().WithMessage("Password cannot be null");
        }
    }
}