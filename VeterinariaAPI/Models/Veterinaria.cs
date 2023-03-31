using System;
using System.Collections.Generic;

namespace VeterinariaAPI.Models;

public partial class Veterinaria
{
    public int VeterinariaId { get; set; }

    public string Ubicacion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Nombre { get; set; } = null!;
}
