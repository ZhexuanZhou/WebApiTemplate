using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TemplateDemo.Core.Entities;

namespace TemplateDemo.Infrastrature.Database
{
    public class DataSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext applicationDbContext, 
            ILoggerFactory loggerFactory, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            int retry = 0
        )
        {
            int retryForAvailability = retry;
            try
            {
                if(!applicationDbContext.Roles.Any())
                {
                    var roles = new List<ApplicationRole>()
                    {
                        new ApplicationRole {Id = Guid.NewGuid(), Name = RoleAndPolicy.RoleName.SuperUser, Description = "Full permission"},
                        new ApplicationRole {Id = Guid.NewGuid(), Name = RoleAndPolicy.RoleName.User, Description = "Limited permission"}
                    };

                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name).Result)
                        {
                            await roleManager.CreateAsync(role);
                        }
                    }
                }

                var admin = new ApplicationUser
                {
                    Id = Guid.NewGuid(), 
                    UserName = "admin@example.com",
                    FirstName = "Khang",
                    LastName = "Tran",
                    Email = "admin@example.com"
                };
                if (!applicationDbContext.Users.Any())
                {
                    var createResult = await userManager
                        .CreateAsync(admin, "Zhexuan363100.");

                    var user = await userManager.FindByNameAsync(admin.UserName);

                    var addRoleResult = userManager
                        .AddToRoleAsync(user, RoleAndPolicy.RoleName.SuperUser)
                        .Result;
                    var addClaimResult =
                        userManager.AddClaimAsync(user, new Claim(RoleAndPolicy.ClaimName.SuperUser, "true")).Result;

                    var logger = loggerFactory.CreateLogger<DataSeed>();
                    if (createResult.Succeeded && addRoleResult.Succeeded && addClaimResult.Succeeded)
                        logger.LogInformation("Super User is created successfully");
                    else
                        logger.LogError("createResult: " + createResult.ToString() + " | roleResult: " + addRoleResult +
                                    " | claimResult: " + addClaimResult);
                }
                await applicationDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<DataSeed>();
                    logger.LogError(ex.Message);
                    
                    await SeedAsync(applicationDbContext, loggerFactory,userManager,roleManager, retryForAvailability);
                }
            }
        }     
    }   
}