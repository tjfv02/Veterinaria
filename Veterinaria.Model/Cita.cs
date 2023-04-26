using System;
using System.Collections.Generic;

namespace Veterinaria.Model;

public partial class Cita
{
    public int CitaId { get; set; }

    public DateTime Fecha { get; set; }

    public int MascotaId { get; set; }

    public int VeterinariaId { get; set; }

    public int VeterinarioId { get; set; }

    //public virtual Mascota Mascota { get; set; } = null!;

    //public virtual Veterinaria Veterinaria { get; set; } = null!;

    //public virtual Veterinario Veterinario { get; set; } = null!;
}
