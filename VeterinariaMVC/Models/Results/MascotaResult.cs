namespace VeterinariaMVC.Models.Results
{
    public class MascotaResult
    {
        public string Message { get; set; }
        public List<Mascota> ListaMascotas { get; set; }
        public Mascota Mascota { get; set; }
    }
}
