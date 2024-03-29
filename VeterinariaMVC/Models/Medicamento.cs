﻿using System;
using System.Collections.Generic;

namespace VeterinariaMVC.Models;

public partial class Medicamento
{
    public int MedicamentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Farmaceutica { get; set; } = null!;

    public virtual ICollection<RecetaMedicina> RecetaMedicinas { get; } = new List<RecetaMedicina>();
}
