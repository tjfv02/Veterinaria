using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Model;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

namespace VeterinariaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly VeterinariaContext _context;
        private readonly string ConnectionString;


        public UsuariosController(VeterinariaContext context, IConfiguration config)
        {
            _context = context;
            
            ConnectionString = config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuarios = await _context.Usuarios
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
                .ToListAsync();
            return usuarios;
        }

        // GET: api/Usuarios/5
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

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            var usuarioExistente = await _context.Usuarios
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

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            // Actualizar los campos del usuario existente
            usuarioExistente.NombreUsuario = usuario.NombreUsuario;
            usuarioExistente.Contraseña = usuario.Contraseña;
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Telefono = usuario.Telefono;
            usuarioExistente.Email = usuario.Email;

            _context.Entry(usuarioExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'VeterinariaContext.Usuarios'  is null.");
          }
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Email = usuario.Email
            };

            _context.Usuarios.Add(nuevoUsuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.UsuarioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario)
        {
            bool registrado;
            string mensaje;

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Email", usuario.Email);
                cmd.Parameters.AddWithValue("Password", usuario.Contraseña);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            return StatusCode(StatusCodes.Status200OK, new { Message = mensaje, Registrado = registrado });



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
                return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "Las contraseñas no coinciden" });

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

            return StatusCode(StatusCodes.Status200OK, new { Message = mensaje, Registrado = registrado });
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
                return StatusCode(StatusCodes.Status200OK, new { Message = "SignIn Exitoso", Login = Login , Usuario = nuevoUsuario.Result.Value });

            }

            return StatusCode(StatusCodes.Status401Unauthorized, new { Message = "Correo o Contraseña Incorectos", Login = Login });
            
        }

        public static string EncryptPassword(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(password));

                foreach (byte b in result)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
    }
}
