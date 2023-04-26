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
        private bool signUp = false;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        public ActionResult SignIn()
        {
            ViewBag.SignUp = signUp;
            ViewBag.Error = false;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(Usuario usuario)
        {
            if (usuario.Email == null || usuario.Contraseña == null)
            {
                ViewData["Mensaje"] = "Tienes que Ingresar los datos para poder Iniciar Sesión";
                ViewBag.Error = true;
                return View();
            }
            await _authService.SignIn(usuario);

            if (!_authService.Auth)
            {
                ViewData["Mensaje"] = "Correo o Contraseña Incorrectos";
                ViewBag.Error = true;
                return View();
            }
            HttpContext.Session.SetString("Usuario", usuario.Email);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            ViewBag.Error = false;
            ViewBag.SignUp = signUp;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(Usuario usuario)
        {
            var nuevoUsuario = new Usuario
            {
                Contraseña = usuario.Contraseña,
                Email = usuario.Email,
                ConfirmarPassword = usuario.ConfirmarPassword
            };
            if (usuario.NombreUsuario == null || usuario.Nombre == null || usuario.Apellido == null || usuario.Email == null || usuario.Contraseña == null)
            {
                ViewData["Mensaje"] = "Tienes que ingresar todos los datos necesarios para hacer el registro";
                ViewBag.Error = true;
                return View();
            }

            if (nuevoUsuario.Contraseña != usuario.ConfirmarPassword)
            {
                ViewData["Mensaje"] = "Las Contraseñas no Coinciden";
                ViewBag.Error = true;
                return View();

            }
            await _authService.SignUp(usuario);

            if (!_authService.Auth)
            {
                ViewBag.Error = true;
                ViewData["Mensaje"] = "Ya existe un Usuario con ese correo  ";
                return View();
            }
            

            ViewData["Mensaje"] = "Usuario creado con Éxito";
            signUp = true;
            ViewBag.SignUp = signUp;
            return RedirectToAction("SignIn", "Login");
        }

      
    }
}
