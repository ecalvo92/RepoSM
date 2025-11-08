USE [master]
GO

CREATE DATABASE [BD_SM]
GO

USE [BD_SM]
GO

CREATE TABLE [dbo].[tbError](
	[ConsecutivoError] [int] IDENTITY(1,1) NOT NULL,
	[ConsecutivoUsuario] [int] NOT NULL,
	[Mensaje] [varchar](max) NOT NULL,
	[Origen] [varchar](255) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
 CONSTRAINT [PK_tbError] PRIMARY KEY CLUSTERED 
(
	[ConsecutivoError] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbPerfil](
	[ConsecutivoPerfil] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tbPerfil] PRIMARY KEY CLUSTERED 
(
	[ConsecutivoPerfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbProducto](
	[ConsecutivoProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](2000) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
	[Imagen] [varchar](255) NOT NULL,
 CONSTRAINT [PK_tbProducto] PRIMARY KEY CLUSTERED 
(
	[ConsecutivoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbUsuario](
	[ConsecutivoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](10) NOT NULL,
	[Estado] [bit] NOT NULL,
	[ConsecutivoPerfil] [int] NOT NULL,
 CONSTRAINT [PK_tbUsuario] PRIMARY KEY CLUSTERED 
(
	[ConsecutivoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tbError] ON 
GO
INSERT [dbo].[tbError] ([ConsecutivoError], [ConsecutivoUsuario], [Mensaje], [Origen], [FechaHora]) VALUES (1, 0, N'Could not find stored procedure ''ValidarUsu''.', N'/api/Home/RecuperarAcceso', CAST(N'2025-10-18T08:50:28.500' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tbError] OFF
GO

SET IDENTITY_INSERT [dbo].[tbPerfil] ON 
GO
INSERT [dbo].[tbPerfil] ([ConsecutivoPerfil], [Nombre]) VALUES (1, N'Usuario Administrador')
GO
INSERT [dbo].[tbPerfil] ([ConsecutivoPerfil], [Nombre]) VALUES (2, N'Usuario Regular')
GO
SET IDENTITY_INSERT [dbo].[tbPerfil] OFF
GO

SET IDENTITY_INSERT [dbo].[tbProducto] ON 
GO
INSERT [dbo].[tbProducto] ([ConsecutivoProducto], [Nombre], [Descripcion], [Precio], [Estado], [Imagen]) VALUES (1, N'Caja de fresas', N'Fresas de temporada de este año', CAST(2500.00 AS Decimal(10, 2)), 1, N'/imagenes/')
GO
INSERT [dbo].[tbProducto] ([ConsecutivoProducto], [Nombre], [Descripcion], [Precio], [Estado], [Imagen]) VALUES (2, N'Caja de fresas pequeña', N'fruta de temporada de fresas pequeñillas', CAST(680.00 AS Decimal(10, 2)), 1, N'/imagenes/')
GO
SET IDENTITY_INSERT [dbo].[tbProducto] OFF
GO

SET IDENTITY_INSERT [dbo].[tbUsuario] ON 
GO
INSERT [dbo].[tbUsuario] ([ConsecutivoUsuario], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado], [ConsecutivoPerfil]) VALUES (1, N'208690734', N'DIEGO ARMANDO PICADO MURILLO', N'dpicado90734@ufide.ac.cr', N'Saprissa', 1, 2)
GO
INSERT [dbo].[tbUsuario] ([ConsecutivoUsuario], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado], [ConsecutivoPerfil]) VALUES (2, N'117100315', N'MEJIA FERNANDEZ ADRIAN', N'amejia00315@ufide.ac.cr', N'123', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[tbUsuario] OFF
GO

ALTER TABLE [dbo].[tbUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tbUsuario_tbPerfil] FOREIGN KEY([ConsecutivoPerfil])
REFERENCES [dbo].[tbPerfil] ([ConsecutivoPerfil])
GO
ALTER TABLE [dbo].[tbUsuario] CHECK CONSTRAINT [FK_tbUsuario_tbPerfil]
GO

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
	@ConsecutivoUsuario int,
    @Contrasenna varchar(10)
AS
BEGIN
	
    UPDATE  dbo.tbUsuario
    SET     Contrasenna = @Contrasenna
    WHERE   ConsecutivoUsuario = @ConsecutivoUsuario

END
GO

CREATE PROCEDURE [dbo].[ActualizarPerfil]
	@ConsecutivoUsuario int,
    @Identificacion varchar(15),
    @Nombre varchar(255),
    @CorreoElectronico varchar(100)
AS
BEGIN
	
    UPDATE  dbo.tbUsuario
    SET     Identificacion = @Identificacion,
            Nombre = @Nombre,
            CorreoElectronico = @CorreoElectronico
    WHERE   ConsecutivoUsuario = @ConsecutivoUsuario

END
GO

CREATE PROCEDURE [dbo].[ConsultarProductos]

AS
BEGIN
	
    SELECT  ConsecutivoProducto,
            Nombre,
            Precio,
            Estado,
            Imagen
      FROM  dbo.tbProducto

END
GO

CREATE PROCEDURE [dbo].[ConsultarUsuario]
    @ConsecutivoUsuario INT
AS
BEGIN
	
    SELECT  ConsecutivoUsuario,
            Identificacion,
            U.Nombre,
            CorreoElectronico,
            Contrasenna,
            Estado,
            U.ConsecutivoPerfil,
            P.Nombre 'NombrePerfil'
      FROM  dbo.tbUsuario U
      INNER JOIN dbo.tbPerfil P ON U.ConsecutivoPerfil = P.ConsecutivoPerfil
      WHERE ConsecutivoUsuario = @ConsecutivoUsuario

END
GO

CREATE PROCEDURE [dbo].[RegistrarError]
	@ConsecutivoUsuario INT, 
	@Mensaje VARCHAR(MAX), 
	@Origen VARCHAR(255)
AS
BEGIN
	
    INSERT INTO dbo.tbError (ConsecutivoUsuario,Mensaje,Origen,FechaHora)
    VALUES (@ConsecutivoUsuario, @Mensaje, @Origen, GETDATE())

END
GO

CREATE PROCEDURE [dbo].[Registro]
	@Identificacion VARCHAR(15),
    @Nombre VARCHAR(255),
    @CorreoElectronico VARCHAR(100),
    @Contrasenna VARCHAR(10)
AS
BEGIN
	
    DECLARE @Estado BIT = 1
    DECLARE @Perfil INT = 2

    IF NOT EXISTS(SELECT 1 FROM dbo.tbUsuario 
        WHERE   Identificacion = @Identificacion
            OR  CorreoElectronico = @CorreoElectronico)
    BEGIN

        INSERT INTO dbo.tbUsuario (Identificacion,Nombre,CorreoElectronico,Contrasenna,Estado,ConsecutivoPerfil)
        VALUES (@Identificacion,@Nombre,@CorreoElectronico,@Contrasenna,@Estado,@Perfil)
    
    END

END
GO

CREATE PROCEDURE [dbo].[RegistroProductos]
	@Nombre VARCHAR(100),
    @Descripcion VARCHAR(2000),
    @Precio DECIMAL(10,2),
    @Imagen VARCHAR(255)
AS
BEGIN
	
    DECLARE @Estado BIT = 1

    IF NOT EXISTS(SELECT 1 FROM dbo.tbProducto 
        WHERE   Nombre = @Nombre)
    BEGIN

        INSERT INTO dbo.tbProducto(Nombre,Descripcion,Precio,Estado,Imagen)
        VALUES (@Nombre, @Descripcion, @Precio, @Estado, @Imagen)

        SELECT @@IDENTITY 'ConsecutivoProducto'
    
    END
    ELSE
    BEGIN

        SELECT 0 'ConsecutivoProducto'

    END

END
GO

CREATE PROCEDURE [dbo].[ValidarInicioSesion]
    @CorreoElectronico VARCHAR(100),
    @Contrasenna VARCHAR(10)
AS
BEGIN
	
    SELECT  ConsecutivoUsuario,
            Identificacion,
            U.Nombre,
            CorreoElectronico,
            Contrasenna,
            Estado,
            U.ConsecutivoPerfil,
            P.Nombre 'NombrePerfil'
      FROM  dbo.tbUsuario U
      INNER JOIN dbo.tbPerfil P ON U.ConsecutivoPerfil = P.ConsecutivoPerfil
      WHERE CorreoElectronico = @CorreoElectronico
        AND Contrasenna = @Contrasenna
        AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[ValidarUsuario]
    @CorreoElectronico VARCHAR(100)
AS
BEGIN
	
    SELECT  ConsecutivoUsuario,
            Identificacion,
            Nombre,
            CorreoElectronico,
            Contrasenna,
            Estado,
            ConsecutivoPerfil
      FROM  dbo.tbUsuario
      WHERE CorreoElectronico = @CorreoElectronico
        AND Estado = 1

END
GO