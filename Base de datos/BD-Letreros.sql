Create database ContLetreros

use ContLetreros

create table Usuarios(
ID_Usuario INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Usuario nvarchar(15),
Contrase�a nvarchar(20),
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
Descripci�n VARCHAR(255), -- Descripci�n detallada del material
Categor�a	VARCHAR(100), -- Clasificaci�n del material (ej: vinilos, LEDs, etc.)
Cantidad DECIMAL, --Cantidad actual en stock
UnidadMedida VARCHAR(50), -- Unidad de medida (metros, kilogramos, piezas, etc.)
CostoUnitario DECIMAL, -- Costo por unidad de medida del material
FechaCompra	DATE, -- Fecha en que se compr� el material
FechaCaducidad DATE
)

CREATE TABLE Pedidos (
    PedidoID INT IDENTITY (1,1) PRIMARY KEY,
    ClienteID INT,
    NombreCliente VARCHAR(255),
    Empleado VARCHAR(50),
    FechaPedido DATE,
    FechaEntrega DATE,
    Total DECIMAL(10,2),
    Pagado BIT,
);

CREATE TABLE DetallePedido (
    DetallePedidoID INT IDENTITY (1,1) PRIMARY KEY,
    PedidoID INT,
    ProductoID INT,
    Producto VARCHAR(255),
    Descripci�nProducto VARCHAR(255),
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    Subtotal DECIMAL(10,2),
);


CREATE TABLE Compras (
    CompraID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
    FechaCompra DATE, -- Fecha de la compra
    TotalCompra DECIMAL(10,2), -- Total de la compra
);


CREATE TABLE DetalleCompras (
    CompraID INT, -- Identificador de la compra
    ID_MateriaPrima INT, -- Identificador de la materia prima
    MateriaPrima varchar(255), -- Identificador de la materia prima
    Cantidad DECIMAL, -- Cantidad comprada
    CostoUnitario DECIMAL(10,2), -- Costo por unidad de medida del material
    Total DECIMAL(10,2), -- Total por el material
);



drop table DetalleCompras

select * from DetalleCompras

select * from Compras

delete from DetalleCompras

drop database ContAlumnos