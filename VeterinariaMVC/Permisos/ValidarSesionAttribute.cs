using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VeterinariaMVC.Permisos
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var httpContext = context.HttpContext; // Obtener la instancia del objeto HttpContext

            if (httpContext.Session.GetString("Usuario") == "")
            {
                context.Result = new RedirectResult("~/Login/SignIn");
            }

            base.OnActionExecuting(context);
        }
    }
}
