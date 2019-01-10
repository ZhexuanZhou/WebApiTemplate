using AutoMapper;
using TemplateDemo.Core.Entities;
using TemplateDemo.Infrastrature.ViewModels;

namespace TemplateDemo.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationViewModel, ApplicationUser>()
                .ForMember(dest=>dest.FirstName, opt=>opt.MapFrom(src=>src.FirstName))
                .ForMember(dest=>dest.LastName, opt=>opt.MapFrom(src=>src.LastName))
                .ForMember(dest=>dest.Gender, opt=>opt.MapFrom(src=>src.Gender))
                .ForMember(dest=>dest.UserName, opt=>opt.MapFrom(src=>src.Email))
                .ForMember(dest=>dest.Email, opt=>opt.MapFrom(src=>src.Email));
        }
    }
}