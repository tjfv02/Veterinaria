using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public interface ICitaService
    {
        Task<List<Cita>> List();
        Task<Cita> Get(int citaId);
        Task<bool> Save(Cita cita);
        Task<bool> Edit(Cita cita);
        Task<bool> Delete(int citaId);
    }
}
