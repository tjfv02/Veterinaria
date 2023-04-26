using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Model;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;

    //public virtual ICollection<Mascota> Mascota { get; } = new List<Mascota>();

    public string? ConfirmarPassword { get; set; }
}
