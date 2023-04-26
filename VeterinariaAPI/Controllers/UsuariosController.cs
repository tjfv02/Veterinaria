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
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly VeterinariaContext _context;


        public UsuariosController(VeterinariaContext context)
        {
            _context = context;
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

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
    }
}
