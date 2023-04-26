using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinariaMVC.Models;
using VeterinariaMVC.Permisos;
using VeterinariaMVC.Services;

namespace VeterinariaMVC.Controllers
{
    [ValidarSesion]
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IMascotaService _mascotaService;
        private readonly IVeterinariaService _veterinariaService;
        private readonly IVeterinarioService _veterinarioService;

        public CitasController(ICitaService citaService, IMascotaService mascotaService, IVeterinariaService veterinariaService, IVeterinarioService veterinarioService)
        {
            _citaService = citaService;
            _mascotaService = mascotaService;
            _veterinariaService = veterinariaService;
            _veterinarioService = veterinarioService;
        }

        public async Task<IActionResult> Index()
        {
            List<Cita> Lista = await _citaService.List();
            return View(Lista);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _citaService.Get(id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        public async Task<IActionResult> Create()
        {
            List<Mascota> ListaMascota = await _mascotaService.List();
            List<Veterinario> ListaVeterinario = await _veterinarioService.List();
            List<Veterinaria> ListaVeterinaria = await _veterinariaService.List();
            ViewData["MascotaId"] = new SelectList(ListaMascota, "MascotaId", "Nombre");
            ViewData["VeterinarioId"] = new SelectList(ListaVeterinario, "VeterinarioId", "Nombre");
            ViewData["VeterinariaId"] = new SelectList(ListaVeterinaria, "VeterinariaId", "Nombre");
            return View();
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaId,Fecha,MascotaId,VeterinariaId,VeterinarioId")] Cita cita)
        {
            bool respuesta;

            List<Mascota> ListaMascota = await _mascotaService.List();
            List<Veterinario> ListaVeterinario = await _veterinarioService.List();
            List<Veterinaria> ListaVeterinaria = await _veterinariaService.List();
            
            respuesta = await _citaService.Save(cita);

            ViewData["MascotaId"] = new SelectList(ListaMascota, "MascotaId", "Nombre", cita.MascotaId);
            ViewData["VeterinarioId"] = new SelectList(ListaVeterinario, "VeterinarioId", "Nombre", cita.VeterinarioId);
            ViewData["VeterinariaId"] = new SelectList(ListaVeterinaria, "VeterinariaId", "Nombre", cita.VeterinariaId);

            return View(cita);
        }

        public async Task<IActionResult> Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cita = await _citaService.Get(id);
            if (cita == null)
            {
                return NotFound();
            }
            return View(cita);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaId,Fecha,MascotaId,VeterinariaId,VeterinarioId")] Cita cita)
        {
            List<Mascota> ListaMascota = await _mascotaService.List();
            List<Veterinario> ListaVeterinario = await _veterinarioService.List();
            List<Veterinaria> ListaVeterinaria = await _veterinariaService.List();

            if (id != cita.CitaId)
            {
                return NotFound();
            }

            try
            {
                var result = await _citaService.Edit(cita);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(cita.CitaId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["MascotaId"] = new SelectList(ListaMascota, "MascotaId", "Nombre", cita.MascotaId);
            ViewData["VeterinarioId"] = new SelectList(ListaVeterinario, "VeterinarioId", "Nombre", cita.VeterinarioId);
            ViewData["VeterinariaId"] = new SelectList(ListaVeterinaria, "VeterinariaId", "Nombre", cita.VeterinariaId);

            return View(cita);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _citaService.Get(id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _citaService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            var cita = _citaService.Get(id);
            return (cita != null);
        }
    }
}
