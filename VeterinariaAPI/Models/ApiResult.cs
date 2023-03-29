using System.Collections.Generic;
namespace VeterinariaAPI.Models
{
    public class ApiResult
    {
        public string Message { get; set; }
        public List<Mascota> Lista { get; set; }
        public Mascota objeto { get; set; }
    }
}
