using System;
using System.Collections.Generic;

namespace VeterinariaMVC.Models;

public partial class Mascota
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public double Peso { get; set; }

    public int UsuarioId { get; set; }

    //public virtual ICollection<Cita> Cita { get; } = new List<Cita>();

    public virtual Usuario Usuario { get; set; } = null!;
}
