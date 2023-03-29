using System;
using System.Collections.Generic;

namespace VeterinariaAPI.Models;

public partial class Mascota
{
    public int MascotaId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public double Peso { get; set; }

    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; }
}
