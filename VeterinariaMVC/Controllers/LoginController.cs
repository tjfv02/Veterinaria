using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using VeterinariaMVC.Models;
using VeterinariaMVC.Models.Results;
using VeterinariaMVC.Services;
using VeterinariaMVC.Services.Auth;

namespace VeterinariaMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        public ActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(Usuario usuario)
        {
            await _authService.SignIn(usuario);

            if (!_authService.Auth)
            {
                ViewData["Mensaje"] = _authService.Mensaje;
                return View();
            }
            HttpContext.Session.SetString("Usuario", usuario.Email);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Usuario usuario)
        {

            return View();
        }

      
    }
}
