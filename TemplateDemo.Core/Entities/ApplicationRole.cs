using System;
using Microsoft.AspNetCore.Identity;

namespace TemplateDemo.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description{get;set;}
    }
}