using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateDemo.Api.Controllers
{
    [Route("api/home")]
    [Authorize]
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return Ok("Authrized");
        }
    }
}