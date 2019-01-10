using System;
using Microsoft.AspNetCore.Identity;

namespace TemplateDemo.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Gender{get;set;}
    }
}