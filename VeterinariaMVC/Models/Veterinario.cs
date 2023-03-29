﻿using System;
using System.Collections.Generic;

namespace VeterinariaMVC.Models;

public partial class Veterinario
{
    public int VeterinarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int VeterinariaId { get; set; }

}
