if DB_ID('DB_Venta') is not null
Begin
  use master 
  drop database DB_Venta
End

create database DB_Venta
go

use DB_Venta
go

create table Rol(
	id_rol int identity primary key,
	nombre_rol varchar(60) unique
)
go

create table Usuario(
	id_user int identity primary key,
	nombres varchar(70),
	apellidos varchar(70),
	correo varchar(60),
	fechaNac date,
	sueldo decimal(10,2),
	genero char(1),
	id_rol int,
	estado int ,
	foreign key(id_rol) references Rol(id_rol)
)
go

create table Producto(
	id_prod int identity primary key,
	nombre varchar(100),
	precio decimal(10,2),
	stock int,
	estado int,
	imagen varchar(50)
)
go


insert into Rol values('ADMIN')
insert into Rol values('EMPLEADO')

insert into Usuario values('Juan','Campos','juan@gmail.com',GETDATE(), 1500.512,'M',1 , 1)
go


create procedure sp_mant_usuarios
(
@nId_user int = 0,
@cNombres varchar(70) = '',
@cApellidos varchar(70) = '',
@cCorreo varchar(60) = '',
@dFechaNac date = null,
@nSueldo real = 0,
@cGenero char(1) = '',
@nId_rol int = 0,
@nEstado int,
@nOpc int = 0
)
as
begin
	if @nOpc = 1
	  begin
		insert into Usuario(nombres,apellidos,correo,fechaNac,sueldo,genero,id_rol,estado)
		values(@cNombres , @cApellidos, @cCorreo ,@dFechaNac ,@nSueldo , @cGenero , @nId_rol, @nEstado)
	  end
	else if @nOpc = 2
	  begin
		update Usuario set nombres = @cNombres ,apellidos = @cApellidos, estado = @nEstado,
		correo = @cCorreo ,fechaNac = @dFechaNac,sueldo = @nSueldo,genero = @cGenero , id_rol = @nId_rol
		where id_user = @nId_user
	  end
   else if @nOpc = 3
	  begin
		delete from Usuario
		where id_user = @nId_user
	  end
end
go

create procedure sp_mant_productos
(
@nId_prod int ,
@cNombre varchar(100),
@nPrecio decimal(10,2),
@nStock int,
@nEstado int,
@cImagen varchar(50),
@nOpc int = 0
)
as
begin
	if @nOpc = 1
	  begin
		insert into Producto(nombre,precio,stock,estado,imagen)
		values(@cNombre,@nPrecio,@nStock,@nEstado,@cImagen)
	  end
	else if @nOpc = 2
	  begin
		update Producto set nombre = @cNombre,precio = @nPrecio,stock = @nStock,
		estado = @nEstado,imagen = @cImagen
		where id_prod = @nId_prod
	  end
	else if @nOpc = 3
	  begin
		delete from Producto 
		where id_prod = @nId_prod
	  end
end
go


create procedure sp_existeCorreo(@id int ,@correo varchar(50) )
as
begin
	select count(1) 
	from Usuario 
	where (id_user != @id or @id = 0) and correo = @correo
end
go

-- select * from usuario
