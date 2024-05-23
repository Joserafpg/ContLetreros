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

CREATE TABLE Pedidos (
    PedidoID INT IDENTITY (1,1) PRIMARY KEY,
    ClienteID INT,
    NombreCliente VARCHAR(255),
    Empleado VARCHAR(50),
    Ancho DECIMAL (10,2),
    Largo DECIMAL(10,2),
	Precio_material DECIMAL (10,2),
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
    DescripciónProducto VARCHAR(255),
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





CREATE PROCEDURE ContarPedidosNoPagados
AS
BEGIN
    SELECT COUNT(*) AS TotalNoPagados
    FROM Pedidos
    WHERE Pagado = 0;
END;


EXEC ContarPedidosNoPagados;



CREATE PROCEDURE ContarPedidosCompletadosMesActual
AS
BEGIN
    SELECT COUNT(*) AS TotalPedidosCompletados
    FROM Pedidos
    WHERE Pagado = 1
      AND YEAR(FechaEntrega) = YEAR(GETDATE())
      AND MONTH(FechaEntrega) = MONTH(GETDATE());
END;

EXEC ContarPedidosCompletadosMesActual;



CREATE PROCEDURE SumarCantidadTotalProductos
AS
BEGIN
    SELECT SUM(Cantidad) AS CantidadTotalProductos
    FROM Inventario;
END;


EXEC SumarCantidadTotalProductos;




drop procedure ContarProductosEnExistencia

select * from Pedidos

select * from DetallePedido

delete from DetalleCoDmpras

drop TABLE Pedidos