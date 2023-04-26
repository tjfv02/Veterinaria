using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Model;

namespace VeterinariaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] Usuario usuario)
        {
            return null;
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn([FromBody] LoginUsers request)
        {
            return null;
        }
    }
}
