USE [master]
GO

CREATE DATABASE [SM_BD]
GO

USE [SM_BD]
GO

CREATE TABLE [dbo].[tbError](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Mensaje] [varchar](max) NOT NULL,
	[Lugar] [varchar](50) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[ConsecutivoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_tbError] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbUsuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_tbUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tbError] ON 
GO
INSERT [dbo].[tbError] ([Consecutivo], [Mensaje], [Lugar], [FechaHora], [ConsecutivoUsuario]) VALUES (1, N'Procedure or function ''spRegistrarUsuario'' expects parameter ''@Contrasenna'', which was not supplied.', N'Home - RegistroAPI', CAST(N'2026-06-13T08:41:41.380' AS DateTime), 0)
GO
INSERT [dbo].[tbError] ([Consecutivo], [Mensaje], [Lugar], [FechaHora], [ConsecutivoUsuario]) VALUES (2, N'Procedure or function ''spRegistrarUsuario'' expects parameter ''@Contrasenna'', which was not supplied.', N'/api/Home/RegistroAPI', CAST(N'2026-06-13T08:56:41.357' AS DateTime), 0)
GO
INSERT [dbo].[tbError] ([Consecutivo], [Mensaje], [Lugar], [FechaHora], [ConsecutivoUsuario]) VALUES (3, N'Procedure or function ''spIniciarSesionUsuario'' expects parameter ''@CorreoElectronico'', which was not supplied.', N'/api/Home/IniciarSesionAPI', CAST(N'2026-06-13T10:44:28.530' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[tbError] OFF
GO

SET IDENTITY_INSERT [dbo].[tbUsuario] ON 
GO
INSERT [dbo].[tbUsuario] ([Consecutivo], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado]) VALUES (1, N'304590415', N'EDUARDO JOSE CALVO CASTILLO', N'ecalvo90415@ufide.ac.cr', N'90415', 1)
GO
INSERT [dbo].[tbUsuario] ([Consecutivo], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado]) VALUES (2, N'402500603', N'LEON CORDERO ESTEFAN', N'eleon00603@ufide.ac.cr', N'00603', 1)
GO
SET IDENTITY_INSERT [dbo].[tbUsuario] OFF
GO

CREATE PROCEDURE [dbo].[spIniciarSesionUsuario]
    @CorreoElectronico  varchar(100),
    @Contrasenna        varchar(100)
AS
BEGIN
	
    SELECT  Consecutivo,
            Identificacion,
            Nombre,
            CorreoElectronico,
            Estado
    FROM    dbo.tbUsuario
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Contrasenna = @Contrasenna
        AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[spRegistrarError]
    @Mensaje            varchar(max),
    @Lugar              varchar(50),
    @FechaHora          datetime,
    @ConsecutivoUsuario int
AS
BEGIN
	
    INSERT INTO dbo.tbError(Mensaje,Lugar,FechaHora,ConsecutivoUsuario)
    VALUES (@Mensaje,@Lugar,@FechaHora,@ConsecutivoUsuario)

END
GO

CREATE PROCEDURE [dbo].[spRegistrarUsuario]
    @Identificacion     varchar(15),
    @Nombre             varchar(250),
    @CorreoElectronico  varchar(100),
    @Contrasenna        varchar(100)
AS
BEGIN
	
    DECLARE @Estado BIT = 1

    INSERT INTO dbo.tbUsuario (Identificacion,Nombre,CorreoElectronico,Contrasenna,Estado)
    VALUES (@Identificacion,@Nombre,@CorreoElectronico,@Contrasenna,@Estado)

END
GO