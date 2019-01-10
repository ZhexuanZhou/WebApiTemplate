using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemplateDemo.Core.Entities;
using TemplateDemo.Core.Interfaces.RepositoryInterfaces;
using TemplateDemo.Infrastrature.Database;
using TemplateDemo.Infrastrature.ViewModels;

namespace TemplateDemo.Api.Controllers
{
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILoggerFactory _loggerFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _loggerFactory = loggerFactory;
        }

        [HttpPost(Name="CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody]RegistrationViewModel model)
        {
            var userIdentity = _mapper.Map<RegistrationViewModel, ApplicationUser>(model);
            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return BadRequest("User exist!");
            }
            var createResult  = await _userManager.CreateAsync(userIdentity, model.Password);

            var user = await _userManager.FindByNameAsync(userIdentity.UserName);
            var addRoleResult = _userManager
                .AddToRoleAsync(user, RoleAndPolicy.RoleName.User)
                .Result;
            var addClaimResult =
                _userManager.AddClaimAsync(user, new Claim(RoleAndPolicy.ClaimName.User, "true")).Result;
            var logger = _loggerFactory.CreateLogger<AccountController>();

            if (createResult.Succeeded && addRoleResult.Succeeded && addClaimResult.Succeeded)
            {   
                var result = "User is created successfully";
                logger.LogInformation(result);
                return Ok(result);
            }
            else
            {
                 var result = "createResult: " + createResult.ToString() + " | roleResult: " + addRoleResult +
                            " | claimResult: " + addClaimResult;
                 logger.LogError(result);
                 return Ok(result);
            }   
        }
    }
}