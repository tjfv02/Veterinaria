ALTER PROC SP_RegistrarUsuario(
	@NombreUsuario varchar(40),
	@Nombre varchar(40),
	@Apellido varchar(40),
	@Telefono varchar(8),
	@Email varchar(40),
	@Contraseña varchar(40),
	@Registrado bit output,
	@Mensaje varchar(100) output
)
AS
BEGIN
	IF(not exists(SELECT * FROM Usuario WHERE Email = @Email))
	BEGIN
		INSERT INTO Usuario(Usuario,Contraseña,Nombre,Apellido,Telefono,Email) VALUES(@NombreUsuario,@Contraseña,@Nombre,@Apellido,@Telefono,@Email)
		SET @Registrado = 1
		SET @Mensaje = 'Usuario Registrado Correctamente'
	END
	ELSE
	BEGIN
		SET @Registrado = 0
		SET @Mensaje = 'Este correo ya ha sido Registrado con otra cuenta'
	END

END

CREATE PROC SP_ValidarUsuario(
	@Email varchar(40),
	@Password varchar(40)
)
AS
BEGIN	
	IF(exists(SELECT * FROM Usuario WHERE Email = @Email and Contraseña = @Password))
	BEGIN
		Select UsuarioID FROM Usuario WHERE Email = @Email and Contraseña = @Password
	END
	ELSE
	BEGIN
		SELECT '0'
	END
END