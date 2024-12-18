use master
drop database if exists Ecommerce_DB;

-- Crear la base de datos
CREATE DATABASE Ecommerce_DB;
GO

USE Ecommerce_DB;
GO

-- Tabla Usuario
CREATE TABLE Usuarios (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    correo VARCHAR(50) NOT NULL UNIQUE,
    clave VARCHAR(50) NOT NULL,
    direccion VARCHAR(200),
    telefono VARCHAR(25),
    localidad VARCHAR(100), 
    fecha_nacimiento DATE,
    idRol INT DEFAULT 1, -- 1: cliente, 2: admin   
    estado BIT DEFAULT 1
);
GO

-- Tabla Categorias
CREATE TABLE Categorias (
    idCategoria INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    estado BIT DEFAULT 1
    
);
GO

-- Tabla Articulo
CREATE TABLE Articulos (
    idArticulo INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(200),
    precio DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    categoria_id INT NOT NULL, -- Permitir NULL para ON DELETE SET NULL
    estado BIT DEFAULT 1
    CONSTRAINT FK_Articulo_Categoria FOREIGN KEY (categoria_id) REFERENCES Categorias(idCategoria)
);
GO

-- Tabla Carrito
CREATE TABLE Carritos (
    idCarrito INT IDENTITY(1,1) PRIMARY KEY,
    idUsuario INT NULL, -- Permitir NULL si se elimina el usuario
    total DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Carrito_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario) 
);
GO

-- Tabla Pedido
CREATE TABLE Pedidos (
    idPedido INT IDENTITY(1,1) PRIMARY KEY,
    idUsuario INT NULL, -- Permitir NULL si se elimina el usuario
    idCarrito INT NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
    fecha_pedido DATETIME DEFAULT GETDATE(),
    idTipoPedido INT NOT NULL DEFAULT 1,
    direccion_envio VARCHAR(255),
    CONSTRAINT FK_Pedido_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario) ,
    CONSTRAINT FK_Pedido_Carrito FOREIGN KEY (idCarrito) REFERENCES Carritos(idCarrito) 
);
GO

-- Tabla intermedia para los art�culos en el carrito
CREATE TABLE Carrito_Articulos (
    idCarritoArticulo INT IDENTITY(1,1) PRIMARY KEY,
    idCarrito INT NOT NULL,
    idArticulo INT NOT NULL,
    cantidad INT NOT NULL,
    CONSTRAINT FK_CarritoArticulo_Carrito FOREIGN KEY (idCarrito) REFERENCES Carritos(idCarrito) ,
    CONSTRAINT FK_CarritoArticulo_Articulo FOREIGN KEY (idArticulo) REFERENCES Articulos(idArticulo) 
);
GO

-- Tabla intermedia para los art�culos en el pedido
CREATE TABLE Pedido_Articulos (
    idPedidoArticulo INT IDENTITY(1,1) PRIMARY KEY,
    idPedido INT NOT NULL,
    idArticulo INT NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_PedidoArticulo_Pedido FOREIGN KEY (idPedido) REFERENCES Pedidos(idPedido) ,
    CONSTRAINT FK_PedidoArticulo_Articulo FOREIGN KEY (idArticulo) REFERENCES Articulos(idArticulo) 
);
GO

CREATE TABLE Imagenes (
    idImagen INT IDENTITY(1,1) PRIMARY KEY,  
    idArticulo INT,                         
    url VARCHAR(200) NOT NULL,               
    CONSTRAINT FK_Imagenes_Articulo FOREIGN KEY (idArticulo) REFERENCES Articulos(idArticulo) 
);
GO
create table tipoPedidos(
    idTipoPedido int identity(1,1) primary key,
    nombre varchar(50) not null,
    estado bit default 1
);
go
create table Roles (
    idRol int primary key,
    Nombre varchar(255) not null
);




-- INSERTs para la tabla Usuario
INSERT INTO Usuarios (nombre, correo, clave, direccion, telefono, localidad, fecha_nacimiento,idrol, estado)
VALUES ('Juan Perez', 'juan.perez@mail.com', 'clave123', 'Calle Falsa 123', '555-1234', 'Buenos Aires', '1985-05-10',1, 1);

INSERT INTO Usuarios (nombre, correo, clave, direccion, telefono, localidad, fecha_nacimiento,idrol, estado)
VALUES ('Maria Lopez', 'maria.lopez@mail.com', 'clave456', 'Av. Siempre Viva 456', '555-5678', 'Cordoba', '1990-09-15',2, 1);
GO

-- INSERTs para la tabla Categorias
INSERT INTO Categorias (nombre)
VALUES ('Regaleria');

INSERT INTO Categorias (nombre)
VALUES ('Indumentaria');
GO

-- INSERTs para la tabla Roles
insert into Roles (idRol, Nombre) values (1, 'Admin');
insert into Roles (idRol, Nombre) values (2, 'Empleado');
insert into Roles (idRol, Nombre) values (3, 'Usuario');

go
-- INSERTs para la tabla Articulo
INSERT INTO [Articulos] ([nombre], [descripcion], [precio], [stock], [categoria_id])
VALUES
('Radiograbador Antiguo', 'Radiograbador vintage con casetes y radio AM/FM', 1500.00, 10, 1),
('Televisor CRT', 'Televisor antiguo de tubo con control manual', 2500.00, 5, 1),
('Máquina de Escribir', 'Máquina de escribir mecánica para uso personal', 1200.00, 7, 1),
('Teléfono de Disco', 'Teléfono retro de disco con cable largo', 900.00, 12, 1),
('Vinilo de Música Clásica', 'Disco de vinilo con grandes éxitos de música clásica', 500.00, 20, 1),
('Cámara Fotográfica Analágica', 'Cámara fotográfica de rollo con lente intercambiable', 3500.00, 4, 1),
('Walkman Sony', 'Reproductor de casetes portátil marca Sony', 2200.00, 8, 1),
('Proyector de Diapositivas', 'Proyector para diapositivas de 35mm', 3000.00, 3, 1),
('Reloj Despertador Mecánico', 'Reloj despertador con campana y cuerda manual', 700.00, 15, 1),
('Yoyo antiguo', 'Yoyo antiguo', 900.00, 15, 1),
('Agujereadora manual antigua', 'Agujereadora manual antigua', 1300.00, 9, 1),
('Grabadora de Voz', 'Grabadora de voz con microcasetes', 1300.00, 9, 1),
('Remera de los 90 s', 'Remera retro', 4000.00, 6, 2),
('Bufanda antigua', 'Bufanda retro', 300.00, 30, 2),
('Reloj pulsera retro', 'Reloj pulsera antigup', 800.00, 25, 2);

-- INSERTs para la tabla Carrito
INSERT INTO Carritos (idUsuario, total)
VALUES (1, 20.00); -- carrito de Juan Perez

INSERT INTO Carritos (idUsuario, total)
VALUES (2, 40.00); -- carrito de Maria Lopez
GO

-- INSERTs para la tabla Pedido
INSERT INTO Pedidos (idUsuario, idCarrito, total, direccion_envio)
VALUES (1, 1, 1500.00, 'Calle Falsa 123'); -- pedido de Juan Perez

INSERT INTO Pedidos(idUsuario, idCarrito, total, direccion_envio)
VALUES (2, 2, 300.00, 'Av. Siempre Viva 456'); -- pedido de Maria Lopez
GO

-- INSERTs para la tabla Carrito_Articulo
INSERT INTO Carrito_Articulos (idCarrito, idArticulo, cantidad)
VALUES (1, 1, 1); -- Juan Perez compra 1 laptop

INSERT INTO Carrito_Articulos (idCarrito, idArticulo, cantidad)
VALUES (2, 2, 1); -- Maria Lopez compra 1 aspiradora
GO

-- INSERTs para la tabla Pedido_Articulo
INSERT INTO Pedido_Articulos (idPedido, idArticulo, cantidad, precio_unitario)
VALUES (1, 1, 1, 1000.00); -- 1 laptop en el pedido de Juan Perez

INSERT INTO Pedido_Articulos (idPedido, idArticulo, cantidad, precio_unitario)
VALUES (2, 2, 1, 300.00); -- 1 aspiradora en el pedido de Maria Lopez
GO

-- INSERT para la tabla Imagenes
-- Insertar 15 im�genes para diferentes art�culos
INSERT INTO [Imagenes] ([idArticulo], [url])
VALUES
(1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTF8tB2UL2IhzLBsl0m9Q78tpuQ4xCxeni8Hw&s'),  -- Radiograbador antiguo
(2, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRcM4oUIECQe7EtVHD26GNGWkaWy52FESM1g&s'),  -- televisor crt
(3, 'https://www.hermanoscastano.es/wp-content/uploads/2021/07/HCI07280-scaled-900x1011.jpg'),        -- M�quina de escribir
(4, 'https://www.museocostarica.go.cr/wp-content/grand-media/image/telefono-de-disco.jpg'),          -- Tel�fono de disco
(5, 'https://http2.mlstatic.com/D_NQ_NP_875519-MLA51615080855_092022-O.webp'),   -- Vinilo de m�sica cl�sica
(6, 'https://png.pngtree.com/png-vector/20240529/ourlarge/pngtree-compact-vintage-analog-camera-png-image_12547114.png'),        -- C�mara fotogr�fica anal�gica
(7, 'https://i0.wp.com/hipertextual.com/wp-content/uploads/2019/06/hipertextual-walkman-abuelo-ipod-cumple-40-anos-2019512483-860x573.jpg?resize=780%2C520&quality=70&strip=all&ssl=1'),            -- Walkman Sony
(8, 'https://http2.mlstatic.com/D_NQ_NP_674143-MLA71897377106_092023-O.webp'),  -- Proyector de diapositivas
(9, 'https://i.etsystatic.com/6858355/r/il/0b1900/3599636435/il_570xN.3599636435_itkg.jpg'),      -- Reloj despertador mec�nico
(10, 'https://i.ebayimg.com/thumbs/images/g/1Q4AAOSwrAFm2LUj/s-l1200.jpg'),          -- yoyo antiguo
(11, 'https://i.ebayimg.com/images/g/Hu0AAOSw~P9imjyW/s-l640.jpg'),            -- agujereadora manual antigua
(12, 'https://www.shutterstock.com/image-vector/vector-cassette-recorder-xxl-icon-600nw-75762229.jpg'),      -- gravadora de voz
(13, 'https://acdn.mitiendanube.com/stores/906/459/products/italia1-e1ad25a33da28d316b16002239341759-480-0.jpg'),      -- remera de los 90's
(14, 'https://m.media-amazon.com/images/I/41UVRe4k8LL._AC_SY580_.jpg'), --bufanda antigua
(15, 'https://a.1stdibscdn.com/omega-1940s-retro-rose-gold-and-rubies-bracelet-watch-for-sale/j_94/j_221583921709072820347/j_22158392_1709072821766_bg_processed.jpg');        -- reloj pulsera retro
go
INSERT into tipoPedidos (nombre, estado) values
('En Preparación', 1),
('Enviado', 1),
('Entregado', 1),
('Cancelado', 1);



