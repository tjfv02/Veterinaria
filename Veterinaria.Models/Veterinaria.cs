using System;
using System.Collections.Generic;

namespace Veterinaria.Models;

public class Veterinaria
{
    public int VeterinariaId { get; set; }

    public string Ubicacion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; } = new List<Cita>();
}
