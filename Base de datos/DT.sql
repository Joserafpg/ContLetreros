Create database ContAlumnos

use ContAlumnos

create table Usuarios(
ID_Usuario INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Usuario nvarchar(15),
Contraseņa nvarchar(20),
SuperUsuario bit,
UsuarioComun bit,
Activo bit,
)
