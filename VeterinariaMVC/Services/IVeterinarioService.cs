using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public interface IVeterinarioService
    {
        Task<List<Veterinario>> List();
        Task<Veterinario> Get(int veterinarioId);
        Task<bool> Save(Veterinario veterinario);
        Task<bool> Edit(Veterinario veterinario);
        Task<bool> Delete(int veterinarioId);
    }
}
