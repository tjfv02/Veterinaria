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
    public class MascotumsController : ControllerBase
    {
        private readonly VeterinariaContext _context;

        public MascotumsController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: api/Mascotums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascota()
        {
          if (_context.Mascota == null)
          {
              return NotFound();
          }
            return await _context.Mascota.ToListAsync();
        }

        // GET: api/Mascotums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetMascotum(int id)
        {
          if (_context.Mascota == null)
          {
              return NotFound();
          }
            var mascotum = await _context.Mascota.FindAsync(id);

            if (mascotum == null)
            {
                return NotFound();
            }

            return mascotum;
        }

        // PUT: api/Mascotums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascotum(int id, Mascota mascotum)
        {
            if (id != mascotum.MascotaId)
            {
                return BadRequest();
            }

            _context.Entry(mascotum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotumExists(id))
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

        // POST: api/Mascotums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mascota>> PostMascotum(Mascota mascotum)
        {
          if (_context.Mascota == null)
          {
              return Problem("Entity set 'VeterinariaContext.Mascota'  is null.");
          }
            _context.Mascota.Add(mascotum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MascotumExists(mascotum.MascotaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMascotum", new { id = mascotum.MascotaId }, mascotum);
        }

        // DELETE: api/Mascotums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascotum(int id)
        {
            if (_context.Mascota == null)
            {
                return NotFound();
            }
            var mascotum = await _context.Mascota.FindAsync(id);
            if (mascotum == null)
            {
                return NotFound();
            }

            _context.Mascota.Remove(mascotum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MascotumExists(int id)
        {
            return (_context.Mascota?.Any(e => e.MascotaId == id)).GetValueOrDefault();
        }
    }
}
