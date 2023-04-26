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
    public class MascotasController : Controller
    {
        private readonly IMascotaService _mascotaService;
        private readonly IUsuarioService _usuarioService;

        public MascotasController(IMascotaService mascotaService, IUsuarioService usuarioService)
        {
            _mascotaService = mascotaService;
            _usuarioService = usuarioService;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
        {
            List<Mascota> Lista = await _mascotaService.List();
            return View(Lista);
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _mascotaService.Get(id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public async Task<IActionResult> Create()
        {
            List<Usuario> ListaUsuarios = await _usuarioService.List();
            ViewData["UsuarioId"] = new SelectList(ListaUsuarios, "UsuarioId", "Nombre");
            return View();
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MascotaId,Nombre,Edad,Peso,UsuarioId")] Mascota mascota)
        {
            bool respuesta;
            List<Usuario> ListaUsuarios = await _usuarioService.List();
            respuesta = await _mascotaService.Save(mascota);
            ViewData["UsuarioId"] = new SelectList(ListaUsuarios, "UsuarioId", "Nombre", mascota.UsuarioId);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //List<Usuario> ListaUsuarios = await _usuarioService.List();

            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _mascotaService.Get(id);
            if (mascota == null)
            {
                return NotFound();
            }
            //ViewData["UsuarioId"] = new SelectList(ListaUsuarios, "UsuarioId", "UsuarioId", mascota.UsuarioId);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MascotaId,Nombre,Edad,Peso,UsuarioId")] Mascota mascota)
        {
            List<Usuario> ListaUsuarios = await _usuarioService.List();
            if (id != mascota.MascotaId)
            {
                return NotFound();
            }

            try
            {
                var result = await _mascotaService.Edit(mascota);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotaExists(mascota.MascotaId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["UsuarioId"] = new SelectList(ListaUsuarios, "UsuarioId", "UsuarioId", mascota.UsuarioId);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _mascotaService.Get(id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _mascotaService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
            var mascota = _mascotaService.Get(id);
            return (mascota != null);
        }
    }
}
