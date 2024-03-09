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

create table Estudiantes(
Numero int,
Nombre varchar (20),
Apellido varchar (20),
Sexo varchar (15),
Discapacidad bit,
Curso varchar (5),
Seccion varchar (1),
Area varchar (15),
)

create table Maestros(
Id_Maestro INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Nombre varchar (40),
Curso varchar (5),
Seccion varchar (1),
Area varchar (15),
Materia varchar (40),
)