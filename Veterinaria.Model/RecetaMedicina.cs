﻿using System;
using System.Collections.Generic;

namespace Veterinaria.Model;

public partial class RecetaMedicina
{
    public int RecetaMedicinaId { get; set; }

    public int RecetaMedicaId { get; set; }

    public int MedicamentoId { get; set; }

    //public virtual Medicamento Medicamento { get; set; } = null!;
}
