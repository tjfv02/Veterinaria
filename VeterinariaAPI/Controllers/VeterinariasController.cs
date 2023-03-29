using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinariaAPI.Models;

namespace VeterinariaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeterinariasController : ControllerBase
    {
        private readonly VeterinariaContext _context;

        public VeterinariasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: api/Veterinarias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinaria>>> GetVeterinaria()
        {
          if (_context.Veterinaria == null)
          {
              return NotFound();
          }
            return await _context.Veterinaria.ToListAsync();
        }

        // GET: api/Veterinarias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinaria>> GetVeterinaria(int id)
        {
          if (_context.Veterinaria == null)
          {
              return NotFound();
          }
            var veterinaria = await _context.Veterinaria.FindAsync(id);

            if (veterinaria == null)
            {
                return NotFound();
            }

            return veterinaria;
        }

        // PUT: api/Veterinarias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeterinaria(int id, Veterinaria veterinaria)
        {
            if (id != veterinaria.VeterinariaId)
            {
                return BadRequest();
            }

            _context.Entry(veterinaria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeterinariaExists(id))
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

        // POST: api/Veterinarias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Veterinaria>> PostVeterinaria(Veterinaria veterinaria)
        {
          if (_context.Veterinaria == null)
          {
              return Problem("Entity set 'VeterinariaContext.Veterinaria'  is null.");
          }
            _context.Veterinaria.Add(veterinaria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VeterinariaExists(veterinaria.VeterinariaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVeterinaria", new { id = veterinaria.VeterinariaId }, veterinaria);
        }

        // DELETE: api/Veterinarias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinaria(int id)
        {
            if (_context.Veterinaria == null)
            {
                return NotFound();
            }
            var veterinaria = await _context.Veterinaria.FindAsync(id);
            if (veterinaria == null)
            {
                return NotFound();
            }

            _context.Veterinaria.Remove(veterinaria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeterinariaExists(int id)
        {
            return (_context.Veterinaria?.Any(e => e.VeterinariaId == id)).GetValueOrDefault();
        }
    }
}
