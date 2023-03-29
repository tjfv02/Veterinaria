using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaMVC;
using VeterinariaMVC.Models;
using VeterinariaMVC.Services;

namespace VeterinariaMVC.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly IVeterinarioService _veterinarioService;
        private readonly IVeterinariaService _veterinariaService;

        public VeterinariosController(IVeterinarioService veterinarioService, IVeterinariaService veterinariaService)
        {
            _veterinarioService = veterinarioService;
            _veterinariaService = veterinariaService;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            List<Veterinario> Lista = await _veterinarioService.List();
            return View(Lista);
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _veterinarioService.Get(id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Create
        public async Task<IActionResult> Create()
        {
            List<Veterinaria> ListaVeterinaria = await _veterinariaService.List();
            ViewData["VeterinariaId"] = new SelectList(ListaVeterinaria, "VeterinariaId", "VeterinariaId");
            return View();
        }

        // POST: Veterinarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeterinarioId,Nombre,Apellido,VeterinariaId")] Veterinario veterinario)
        {
            bool respuesta;
            List<Veterinaria> ListaVeterinaria = await _veterinariaService.List();

            respuesta = await _veterinarioService.Save(veterinario);
            ViewData["VeterinariaId"] = new SelectList(ListaVeterinaria, "VeterinariaId", "VeterinariaId");

            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _veterinarioService.Get(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeterinarioId,Nombre,Apellido,VeterinariaId")] Veterinario veterinario)
        {
            if (id != veterinario.VeterinarioId)
            {
                return NotFound();
            }
                try
                {
                    var result = await _veterinarioService.Edit(veterinario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.VeterinarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _veterinarioService.Get(id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _veterinarioService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarioExists(int id)
        {
            var veterinario = _veterinarioService.Get(id);
            return (veterinario != null);
        }
    }
}
