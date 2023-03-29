using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

}
