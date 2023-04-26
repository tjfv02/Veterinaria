using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Model;

public partial class Mascota
{
    [Key]
    public int MascotaId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public double Peso { get; set; }

    public int UsuarioId { get; set; }

    //public virtual ICollection<Cita> Cita { get; } = new List<Cita>();

    //public virtual Usuario Usuario { get; set; } = null!;
}
