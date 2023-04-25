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

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Usuario usuario)
        {
            bool registrado;
            string mensaje;

            //if (usuario.Password != usuario.ConfirmarPassword)
            //{
            //    ViewData["Mensaje"] = "Las contraseñas no coinciden";
            //    return View();
            //}

            //usuario.Password = EncryptPassword(usuario.Password);

            return View();
        }

        public static string EncryptPassword(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using(SHA256 hash = SHA256.Create()) {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(password));

                foreach (byte b in result)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }
    }
}
