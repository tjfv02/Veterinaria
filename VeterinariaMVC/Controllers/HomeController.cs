using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VeterinariaMVC.Models;
using VeterinariaMVC.Permisos;
using VeterinariaMVC.Services;

namespace VeterinariaMVC.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService;


        public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            List<Usuario> ListaUsuarios = await _usuarioService.List();

            var usuarioBuscado = ListaUsuarios.FirstOrDefault(u => u.Email == usuario);

            return View(usuarioBuscado);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("Usuario",null);

            return RedirectToAction("SignIn", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}