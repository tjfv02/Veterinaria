using System;
using System.Collections.Generic;

namespace Veterinaria.Model;

public partial class RecetaMedica
{
    public int RecetaMedicaId { get; set; }

    public DateTime Fecha { get; set; }

    public string Medicina { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int MascotaId { get; set; }

    public int VeterinarioId { get; set; }
}
