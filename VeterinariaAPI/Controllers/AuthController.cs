using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using VeterinariaAPI.Models;
using System.Text;

namespace VeterinariaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string SecretKey;
        private readonly string TokenExpiration;
        private readonly VeterinariaContext _context;


        public AuthController(IConfiguration config)
        {
            SecretKey = config.GetSection("JWT").GetSection("SecretKey").ToString();
            TokenExpiration = config.GetSection("JWT").GetSection("Expiration").Value;

        }

        [HttpPost]
        [Route("Validation")]
        public IActionResult Validation([FromBody] LoginUsers request)
        {
            if (request.Email == "tjfv02@gmail.com" && request.Password == "251100") //validate
            {
                var keyBytes = Encoding.ASCII.GetBytes(SecretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(TokenExpiration)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreate = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { Message = "Autorizado", token = tokenCreate });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "Sin Autorización", token = ""});

            }
        }
    }
}
