CREATE DATABASE VeterinariaTest
use VeterinariaTest

CREATE TABLE [Usuario] (
	UsuarioID int  IDENTITY(1,1) NOT NULL,
	Usuario varchar(40),
	Contraseña varchar(40) NOT NULL,
	Nombre varchar(40),
	Apellido varchar(40),
	Telefono varchar(8),
	Email varchar(40) NOT NULL,
  CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED
  (
  [UsuarioID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Mascota] (
	MascotaID int  IDENTITY(1,1) NOT NULL,
	Nombre varchar(40) NOT NULL,
	Edad int NOT NULL,
	Peso float NOT NULL,
	UsuarioID int NOT NULL,
  CONSTRAINT [PK_MASCOTA] PRIMARY KEY CLUSTERED
  (
  [MascotaID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Cita] (
	CitaID int IDENTITY(1,1) NOT NULL,
	Fecha datetime NOT NULL,
	MascotaID int NOT NULL,
	VeterinariaID int NOT NULL,
	VeterinarioID int NOT NULL,
  CONSTRAINT [PK_CITA] PRIMARY KEY CLUSTERED
  (
  [CitaID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Veterinaria] (
	VeterinariaID int IDENTITY(1,1) NOT NULL,
	Ubicacion varchar(100) NOT NULL,
	Telefono varchar(8) NOT NULL,
	Email varchar(40) NOT NULL,
	Nombre varchar(50) NOT NULL,
  CONSTRAINT [PK_VETERINARIA] PRIMARY KEY CLUSTERED
  (
  [VeterinariaID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Veterinario] (
	VeterinarioID int IDENTITY(1,1) NOT NULL,
	Nombre varchar(40) NOT NULL,
	Apellido varchar(40) NOT NULL,
	VeterinariaID int NOT NULL,
  CONSTRAINT [PK_VETERINARIO] PRIMARY KEY CLUSTERED
  (
  [VeterinarioID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [RecetaMedica] (
	RecetaMedicaID int NOT NULL,
	Fecha datetime NOT NULL,
	Medicina varchar(255) NOT NULL,
	Descripcion varchar(255) NOT NULL,
	MascotaID int NOT NULL,
	VeterinarioID int NOT NULL,
  CONSTRAINT [PK_RECETAMEDICA] PRIMARY KEY CLUSTERED
  (
  [RecetaMedicaID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Medicamento] (
	MedicamentoID int NOT NULL,
	Nombre varchar(255) NOT NULL,
	Farmaceutica varchar(255) NOT NULL,
  CONSTRAINT [PK_MEDICAMENTO] PRIMARY KEY CLUSTERED
  (
  [MedicamentoID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [RecetaMedicina] (
	RecetaMedicinaID int NOT NULL,
	RecetaMedicaID int NOT NULL,
	MedicamentoID int NOT NULL,
  CONSTRAINT [PK_RECETAMEDICINA] PRIMARY KEY CLUSTERED
  (
  [RecetaMedicinaID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

ALTER TABLE [Mascota] WITH CHECK ADD CONSTRAINT [Mascota_fk1] FOREIGN KEY ([UsuarioID]) REFERENCES [Usuario]([UsuarioID])
ON UPDATE CASCADE
GO
ALTER TABLE [Mascota] CHECK CONSTRAINT [Mascota_fk1]
GO

ALTER TABLE [Cita] WITH CHECK ADD CONSTRAINT [Cita_fk0] FOREIGN KEY ([MascotaID]) REFERENCES [Mascota]([MascotaID])
ON UPDATE CASCADE
GO
ALTER TABLE [Cita] CHECK CONSTRAINT [Cita_fk0]
GO
ALTER TABLE [Cita] WITH CHECK ADD CONSTRAINT [Cita_fk1] FOREIGN KEY ([VeterinariaID]) REFERENCES [Veterinaria]([VeterinariaID])
ON UPDATE CASCADE
GO
ALTER TABLE [Cita] CHECK CONSTRAINT [Cita_fk1]
GO
ALTER TABLE [Cita] WITH CHECK ADD CONSTRAINT [Cita_fk2] FOREIGN KEY ([VeterinarioID]) REFERENCES [Veterinario]([VeterinarioID])
ON UPDATE CASCADE
GO
ALTER TABLE [Cita] CHECK CONSTRAINT [Cita_fk2]
GO


ALTER TABLE [Veterinario] WITH CHECK ADD CONSTRAINT [Veterinario_fk0] FOREIGN KEY ([VeterinariaID]) REFERENCES [Veterinaria]([VeterinariaID])
ON UPDATE CASCADE
GO
ALTER TABLE [Veterinario] CHECK CONSTRAINT [Veterinario_fk0]
GO

ALTER TABLE [RecetaMedica] WITH CHECK ADD CONSTRAINT [RecetaMedica_fk0] FOREIGN KEY ([MascotaID]) REFERENCES [Mascota]([MascotaID])
ON UPDATE CASCADE
GO
ALTER TABLE [RecetaMedica] CHECK CONSTRAINT [RecetaMedica_fk0]
GO
ALTER TABLE [RecetaMedica] WITH CHECK ADD CONSTRAINT [RecetaMedica_fk1] FOREIGN KEY ([VeterinarioID]) REFERENCES [Veterinario]([VeterinarioID])
ON UPDATE CASCADE
GO
ALTER TABLE [RecetaMedica] CHECK CONSTRAINT [RecetaMedica_fk1]
GO


ALTER TABLE [RecetaMedicina] WITH CHECK ADD CONSTRAINT [RecetaMedicina_fk0] FOREIGN KEY ([RecetaMedicaID]) REFERENCES [RecetaMedica]([RecetaMedicaID])
ON UPDATE CASCADE
GO
ALTER TABLE [RecetaMedicina] CHECK CONSTRAINT [RecetaMedicina_fk0]
GO
ALTER TABLE [RecetaMedicina] WITH CHECK ADD CONSTRAINT [RecetaMedicina_fk1] FOREIGN KEY ([MedicamentoID]) REFERENCES [Medicamento]([MedicamentoID])
ON UPDATE CASCADE
GO
ALTER TABLE [RecetaMedicina] CHECK CONSTRAINT [RecetaMedicina_fk1]
GO
