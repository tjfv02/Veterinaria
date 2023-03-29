using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public interface IVeterinariaService
    {
        Task<List<Veterinaria>> List();
        Task<Veterinaria> Get(int VeterinariaId);
        Task<bool> Save(Veterinaria Veterinaria);
        Task<bool> Edit(Veterinaria Veterinaria);
        Task<bool> Delete(int VeterinariaId);
    }
}
