using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services.Auth
{
    public interface IAuthService
    {
        Task SignIn(Usuario usuario);
        Task SignUp(Usuario usuario);

    }
}
