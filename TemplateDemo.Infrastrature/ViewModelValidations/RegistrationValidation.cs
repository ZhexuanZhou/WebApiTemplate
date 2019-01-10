using FluentValidation;
using TemplateDemo.Infrastrature.ViewModels;

namespace TemplateDemo.Infrastrature.ViewModelValidations
{
    public class RegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationValidation()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email cannot be nont!");
            RuleFor(x => x.Password).NotNull().WithMessage("Password cannot be nont!");
            RuleFor(x => x.FirstName).NotNull().WithMessage("FirstName cannot be nont!");
            RuleFor(x => x.LastName).NotNull().WithMessage("LastName cannot be nont!");
            RuleFor(x => x.Gender).NotNull().WithMessage("Gender cannot be nont!");
        }
    }
}