using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> List();
        Task<Usuario> Get(int usuarioId);
        Task<bool> Save(Usuario usuario);
        Task<bool> Edit(Usuario usuario);
        Task<bool> Delete(int usuarioId);
    }
}
