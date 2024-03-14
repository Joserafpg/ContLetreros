Create database ContAlumnos

use ContAlumnos

create table Usuarios(
ID_Usuario INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Usuario nvarchar(15),
Contraseña nvarchar(20),
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

create table Curso(
Curso varchar (10),
Seccion varchar (1),
Area varchar (15),
Maestro_Titular varchar (40),
)

create table Maestros(
Id_Maestro INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Nombre varchar (40),
Curso varchar (10),
Seccion varchar (1),
Area varchar (15),
)

create table Materias(
Materia varchar (50),
Maestro varchar (50),
Curso varchar (10),
Seccion varchar (1),
Area varchar (15),
)

SELECT * FROM Maestros
SELECT * FROM Curso
SELECT * FROM Materias


SELECT * FROM Estudiantes WHERE Numero = '30' AND Curso = 'Sexto' AND Seccion = 'A' AND Area = 'Contabilidad'
SELECT Numero, Nombre, Apellido, Sexo, Discapacidad FROM Estudiantes WHERE Curso = 'Sexto' AND Seccion = 'A' AND Area = 'Contabilidad'


Drop table Curso

CREATE PROCEDURE ObtenerEstudiantesPorCursoSeccionYArea 
    @Curso NVARCHAR(50),
    @Seccion NVARCHAR(50),
    @Area NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT e.Numero, e.Nombre, e.Apellido, e.Sexo, e.Discapacidad, c.Maestro_Titular
    FROM Estudiantes e
    INNER JOIN Curso c ON e.Curso = c.Curso AND e.Seccion = c.Seccion AND e.Area = c.Area
    WHERE e.Curso = @Curso AND e.Seccion = @Seccion AND e.Area = @Area
    ORDER BY e.Numero;
END

CREATE PROCEDURE CalcularTotalEstudiantes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS TotalEstudiantes
    FROM Estudiantes;
END



DROP TABLE Maestros

CREATE PROCEDURE CalcularTotalHembras
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS TotalHembras
    FROM Estudiantes
    WHERE Sexo = 'F';
END

CREATE PROCEDURE CalcularTotalVarones
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS TotalVarones
    FROM Estudiantes
    WHERE Sexo = 'M';
END


CREATE PROCEDURE ObtenerMateriasPorCursoSeccionYArea 
    @Curso NVARCHAR(50),
    @Seccion NVARCHAR(50),
    @Area NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Materia, Maestro
    FROM Materias
    WHERE Curso = @Curso AND Seccion = @Seccion AND Area = @Area
    ORDER BY Materia;
END


drop procedure CalcularTotalVarones

EXEC ObtenerEstudiantesPorCursoSeccionYArea 'Sexto', 'A', 'Contabilidad';
EXEC CalcularTotalEstudiantes;
EXEC ObtenerMateriasPorCursoSeccionYArea 'Sexto', 'A', 'Informatica';

