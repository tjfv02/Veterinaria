using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public interface IMascotaService
    {
        Task<List<Mascota>> List();
        Task<Mascota> Get(int mascotaId);
        Task<bool> Save(Mascota mascota);
        Task<bool> Edit(Mascota mascota);
        Task<bool> Delete(int mascotaId);
    }
}
