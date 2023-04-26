using VeterinariaMVC.Models;
using VeterinariaMVC.Models.Results;

namespace VeterinariaMVC.Services.Auth
{
    public interface IAuthService
    {
        Task SignIn(Usuario usuario);
        Task<AuthResult?> SignUp(Usuario usuario);
        string Token { get; }
        bool Auth { get; }
        string Mensaje { get; }

    }
}
