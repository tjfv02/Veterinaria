using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using VeterinariaMVC.Models;
using VeterinariaMVC.Services;

namespace VeterinariaMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public ActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Usuario usuario)
        {

            return View();
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
