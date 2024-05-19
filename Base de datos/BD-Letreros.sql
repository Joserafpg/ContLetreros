Create database ContLetreros

use ContLetreros

create table Usuarios(
ID_Usuario INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Usuario nvarchar(15),
Contraseña nvarchar(20),
SuperUsuario bit,
UsuarioComun bit,
Activo bit,
)


create table Clientes(
ID_Cliente INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Nombre varchar(30),
Apellido varchar(30),
Cedula varchar(11),
Sexo varchar(12),
Fecha_Ingreso date,
)

create table Inventario(
ID_MateriaPrima	INT	IDENTITY (1,1) PRIMARY KEY NOT NULL,
Nombre VARCHAR(255), -- Nombre del material
Descripción VARCHAR(255), -- Descripción detallada del material
Categoría	VARCHAR(100), -- Clasificación del material (ej: vinilos, LEDs, etc.)
Cantidad DECIMAL, --Cantidad actual en stock
UnidadMedida VARCHAR(50), -- Unidad de medida (metros, kilogramos, piezas, etc.)
CostoUnitario DECIMAL, -- Costo por unidad de medida del material
FechaCompra	DATE, -- Fecha en que se compró el material
FechaCaducidad DATE
)

drop table Inventario

select * from Inventario