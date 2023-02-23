using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaMVC;
using VeterinariaMVC.Models;

namespace VeterinariaMVC.Controllers
{
    public class VeterinariasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinariasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veterinarias
        public async Task<IActionResult> Index()
        {
              return _context.Veterinaria != null ? 
                          View(await _context.Veterinaria.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Veterinaria'  is null.");
        }

        // GET: Veterinarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veterinaria == null)
            {
                return NotFound();
            }

            var veterinaria = await _context.Veterinaria
                .FirstOrDefaultAsync(m => m.VeterinariaId == id);
            if (veterinaria == null)
            {
                return NotFound();
            }

            return View(veterinaria);
        }

        // GET: Veterinarias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeterinariaId,Ubicacion,Telefono,Email")] Veterinaria veterinaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinaria);
        }

        // GET: Veterinarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veterinaria == null)
            {
                return NotFound();
            }

            var veterinaria = await _context.Veterinaria.FindAsync(id);
            if (veterinaria == null)
            {
                return NotFound();
            }
            return View(veterinaria);
        }

        // POST: Veterinarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeterinariaId,Ubicacion,Telefono,Email")] Veterinaria veterinaria)
        {
            if (id != veterinaria.VeterinariaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariaExists(veterinaria.VeterinariaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(veterinaria);
        }

        // GET: Veterinarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veterinaria == null)
            {
                return NotFound();
            }

            var veterinaria = await _context.Veterinaria
                .FirstOrDefaultAsync(m => m.VeterinariaId == id);
            if (veterinaria == null)
            {
                return NotFound();
            }

            return View(veterinaria);
        }

        // POST: Veterinarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veterinaria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Veterinaria'  is null.");
            }
            var veterinaria = await _context.Veterinaria.FindAsync(id);
            if (veterinaria != null)
            {
                _context.Veterinaria.Remove(veterinaria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariaExists(int id)
        {
          return (_context.Veterinaria?.Any(e => e.VeterinariaId == id)).GetValueOrDefault();
        }
    }
}
