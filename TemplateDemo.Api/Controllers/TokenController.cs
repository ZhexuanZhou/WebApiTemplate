using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TemplateDemo.Core.Entities;
using TemplateDemo.Infrastrature.ViewModels;

namespace TemplateDemo.Api.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenController(
            IMapper mapper,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost(Name="CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            var userToVerify = await _userManager.FindByNameAsync(login.Email);
            
            if(await _userManager.CheckPasswordAsync(userToVerify, login.Password))
            {
                var tokenString = BuildToken(login);
                response = Ok(new {userId = userToVerify.Id, userName = userToVerify.Email, token = tokenString });
            }

            return response;
        }

        private string BuildToken(LoginViewModel user)
        {

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}