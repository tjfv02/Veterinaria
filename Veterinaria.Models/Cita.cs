using System;
using System.Collections.Generic;

namespace Veterinaria.Models;

public partial class Cita
{
    public int CitaId { get; set; }

    public DateTime Fecha { get; set; }

    public int MascotaId { get; set; }

    public int VeterinariaId { get; set; }

    public int VeterinarioId { get; set; }

    public virtual Mascotum Mascota { get; set; } = null!;

    public virtual Veterinarium Veterinaria { get; set; } = null!;

    public virtual Veterinario Veterinario { get; set; } = null!;
}
