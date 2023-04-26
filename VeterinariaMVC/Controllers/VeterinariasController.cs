using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaMVC;
using VeterinariaMVC.Models;
using VeterinariaMVC.Permisos;
using VeterinariaMVC.Services;

namespace VeterinariaMVC.Controllers
{
    [ValidarSesion]
    public class VeterinariasController : Controller
    {
        private readonly IVeterinariaService _veterinariaService;

        public VeterinariasController(IVeterinariaService veterinariaService)
        {
            _veterinariaService= veterinariaService;
        }

        // GET: Veterinarias
        public async Task<IActionResult> Index()
        {
            List<Veterinaria> Lista = await _veterinariaService.List();
            return View(Lista);
        }

        // GET: Veterinarias/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaria = await _veterinariaService.Get(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeterinariaId,Nombre,Ubicacion,Telefono,Email")] Veterinaria veterinaria)
        {
            bool respuesta;
            respuesta = await _veterinariaService.Save(veterinaria);

            if (!respuesta)
            {
                return NotFound();
            }
            return View(veterinaria);
        }

        // GET: Veterinarias/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaria = await _veterinariaService.Get(id);
            if (veterinaria == null)
            {
                return NotFound();
            }
            return View(veterinaria);
        }

        // POST: Veterinarias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeterinariaId,Nombre,Ubicacion,Telefono,Email")] Veterinaria veterinaria)
        {
            if (id != veterinaria.VeterinariaId)
            {
                return NotFound();
            }

                try
                {
                    var result = _veterinariaService.Edit(veterinaria);
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
            return View(veterinaria);
        }

        // GET: Veterinarias/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinaria = await _veterinariaService.Get(id);
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
            var respuesta = await _veterinariaService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariaExists(int id)
        {
            var veterinaria = _veterinariaService.Get(id);
            return (veterinaria != null);
        }
    }
}
