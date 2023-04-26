using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Veterinaria.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static string SecretKey;
        private readonly string ConnectionString;
        private readonly string TokenExpiration;
        private readonly VeterinariaContext _context;


        public AuthController(VeterinariaContext context, IConfiguration config)
        {
            _context = context;

            ConnectionString = config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            SecretKey = config.GetSection("JWT").GetSection("SecretKey").ToString();
            TokenExpiration = config.GetSection("JWT").GetSection("Expiration").Value;

        }

        [HttpPost]
        [Route("GenerarToken")]
        public string GenerarToken(Usuario request)
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

            return tokenCreate;
            //return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "Sin Autorización", token = ""});
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] Usuario usuario)
        {
            bool registrado;
            string mensaje;

            var nuevoUsuario = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Contraseña = usuario.Contraseña,
                Email = usuario.Email,
            };

            if (nuevoUsuario.Contraseña != usuario.ConfirmarPassword)
            {
                return StatusCode(StatusCodes.Status200OK, new { Message = "Las contraseñas no coinciden", Auth = false });

            }

            nuevoUsuario.Contraseña = EncryptPassword(nuevoUsuario.Contraseña);

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("NombreUsuario", nuevoUsuario.NombreUsuario);
                cmd.Parameters.AddWithValue("Nombre", nuevoUsuario.Nombre);
                cmd.Parameters.AddWithValue("Apellido", nuevoUsuario.Apellido);
                cmd.Parameters.AddWithValue("Telefono", nuevoUsuario.Telefono);
                cmd.Parameters.AddWithValue("Email", nuevoUsuario.Email);
                cmd.Parameters.AddWithValue("Contraseña", nuevoUsuario.Contraseña);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            return StatusCode(StatusCodes.Status200OK, new { Message = mensaje, Auth = registrado });
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn([FromBody] Usuario usuario)
        {
            bool Login = false;
            usuario.Contraseña = EncryptPassword(usuario.Contraseña);

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Email", usuario.Email);
                cmd.Parameters.AddWithValue("Password", usuario.Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                usuario.UsuarioId = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (usuario.UsuarioId != 0)
            {
                Login = true;
                var nuevoUsuario = GetUsuario(usuario.UsuarioId);
                var token = GenerarToken(nuevoUsuario.Result.Value);

                return StatusCode(StatusCodes.Status200OK, new { Message = "SignIn Exitoso", Auth = Login, Token = token });

            }

            return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "Correo o Contraseña Incorectos", Auth = Login });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios
                .Where(u => u.UsuarioId == id)
                .Select(u => new Usuario
                {
                    UsuarioId = u.UsuarioId,
                    NombreUsuario = u.NombreUsuario,
                    Contraseña = u.Contraseña,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Telefono = u.Telefono,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();


            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        public static string EncryptPassword(string password)
        {
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var aesAlg = Aes.Create())
            {
                var pbkdf2 = new Rfc2898DeriveBytes(SecretKey, salt, 1000, HashAlgorithmName.SHA256);
                aesAlg.Key = pbkdf2.GetBytes(32);
                aesAlg.IV = pbkdf2.GetBytes(16);

                using (var encryptor = aesAlg.CreateEncryptor())
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(passwordBytes, 0, passwordBytes.Length);
                            csEncrypt.FlushFinalBlock();

                            byte[] encryptedBytes = msEncrypt.ToArray();
                            return Convert.ToBase64String(encryptedBytes);
                        }
                    }
                }
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);

            using (var aesAlg = Aes.Create())
            {
                var pbkdf2 = new Rfc2898DeriveBytes(SecretKey, salt, 1000, HashAlgorithmName.SHA256);
                aesAlg.Key = pbkdf2.GetBytes(32);
                aesAlg.IV = pbkdf2.GetBytes(16);

                using (var decryptor = aesAlg.CreateDecryptor())
                {
                    using (var msDecrypt = new MemoryStream(encryptedBytes))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
