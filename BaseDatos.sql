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

CREATE TABLE [dbo].[tbEstado](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbEstado] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbRol](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbRol] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbSolicitud](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](150) NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[FechaFinalizacion] [datetime] NULL,
	[ConsecutivoUsuario] [int] NOT NULL,
	[ConsecutivoAdmin] [int] NOT NULL,
	[ConsecutivoEstado] [int] NOT NULL,
 CONSTRAINT [PK_tbSolicitud] PRIMARY KEY CLUSTERED 
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
	[IndicadorTemp] [bit] NOT NULL,
	[ConsecutivoRol] [int] NOT NULL,
 CONSTRAINT [PK_tbUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tbEstado] ON 
GO
INSERT [dbo].[tbEstado] ([Consecutivo], [Nombre]) VALUES (1, N'Abierto')
GO
INSERT [dbo].[tbEstado] ([Consecutivo], [Nombre]) VALUES (2, N'Atendido')
GO
INSERT [dbo].[tbEstado] ([Consecutivo], [Nombre]) VALUES (3, N'Cancelado')
GO
SET IDENTITY_INSERT [dbo].[tbEstado] OFF
GO

SET IDENTITY_INSERT [dbo].[tbRol] ON 
GO
INSERT [dbo].[tbRol] ([Consecutivo], [Nombre]) VALUES (1, N'Usuario')
GO
INSERT [dbo].[tbRol] ([Consecutivo], [Nombre]) VALUES (2, N'Administrador')
GO
SET IDENTITY_INSERT [dbo].[tbRol] OFF
GO

SET IDENTITY_INSERT [dbo].[tbSolicitud] ON 
GO
INSERT [dbo].[tbSolicitud] ([Consecutivo], [Titulo], [Descripcion], [FechaRegistro], [FechaFinalizacion], [ConsecutivoUsuario], [ConsecutivoAdmin], [ConsecutivoEstado]) VALUES (1, N'Error de internet', N'Cuando entro al navegador aparece un dinosaurio', CAST(N'2026-07-27T21:39:34.780' AS DateTime), NULL, 5, 4, 1)
GO
INSERT [dbo].[tbSolicitud] ([Consecutivo], [Titulo], [Descripcion], [FechaRegistro], [FechaFinalizacion], [ConsecutivoUsuario], [ConsecutivoAdmin], [ConsecutivoEstado]) VALUES (2, N'Error de internet', N'Cuando entro al navegador aparece un dinosaurio x2', CAST(N'2026-07-27T21:41:08.093' AS DateTime), NULL, 5, 4, 1)
GO
INSERT [dbo].[tbSolicitud] ([Consecutivo], [Titulo], [Descripcion], [FechaRegistro], [FechaFinalizacion], [ConsecutivoUsuario], [ConsecutivoAdmin], [ConsecutivoEstado]) VALUES (3, N'Error de internet', N'Cuando entro al navegador aparece un dinosaurio x3', CAST(N'2026-07-27T21:41:47.717' AS DateTime), NULL, 5, 6, 1)
GO
INSERT [dbo].[tbSolicitud] ([Consecutivo], [Titulo], [Descripcion], [FechaRegistro], [FechaFinalizacion], [ConsecutivoUsuario], [ConsecutivoAdmin], [ConsecutivoEstado]) VALUES (4, N'Error de internet', N'Cuando entro al navegador aparece un dinosaurio x4', CAST(N'2026-07-27T21:42:02.183' AS DateTime), NULL, 5, 6, 1)
GO
INSERT [dbo].[tbSolicitud] ([Consecutivo], [Titulo], [Descripcion], [FechaRegistro], [FechaFinalizacion], [ConsecutivoUsuario], [ConsecutivoAdmin], [ConsecutivoEstado]) VALUES (5, N'Error de internet', N'Cuando entro al navegador aparece un dinosaurio x5', CAST(N'2026-07-27T21:42:15.927' AS DateTime), NULL, 5, 6, 1)
GO
INSERT [dbo].[tbSolicitud] ([Consecutivo], [Titulo], [Descripcion], [FechaRegistro], [FechaFinalizacion], [ConsecutivoUsuario], [ConsecutivoAdmin], [ConsecutivoEstado]) VALUES (6, N'Error de internet', N'Cuando entro al navegador aparece un dinosaurio x6', CAST(N'2026-07-27T21:42:24.973' AS DateTime), NULL, 5, 4, 1)
GO
SET IDENTITY_INSERT [dbo].[tbSolicitud] OFF
GO

SET IDENTITY_INSERT [dbo].[tbUsuario] ON 
GO
INSERT [dbo].[tbUsuario] ([Consecutivo], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado], [IndicadorTemp], [ConsecutivoRol]) VALUES (4, N'304590415', N'EDUARDO JOSE CALVO CASTILLO', N'ecalvo90415@ufide.ac.cr', N'$2a$11$dUmKzo753u0eXVTsXhJx.ee7VSPco6n.EPyEtKtuxig6.ayZwknzK', 1, 0, 2)
GO
INSERT [dbo].[tbUsuario] ([Consecutivo], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado], [IndicadorTemp], [ConsecutivoRol]) VALUES (5, N'402500603', N' ESTEFAN LEON CORDERO', N'eleon00603@ufide.ac.cr', N'$2a$11$vnpo38WOufW6Ue6t43Fw3u5t3XjAH5kr9TNh.tzZvcUhcNwZIXO5y', 1, 0, 1)
GO
INSERT [dbo].[tbUsuario] ([Consecutivo], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado], [IndicadorTemp], [ConsecutivoRol]) VALUES (6, N'402540724', N'JOSE DANIEL RAMIREZ AGUILAR', N'jdaniel.ramlar@gmail.com', N'$2a$11$pUkuE34EC3rY5QhnQb54T.1G1SiHWskaBsgRjCe9n7rW.ex3w3yHK', 1, 0, 2)
GO
SET IDENTITY_INSERT [dbo].[tbUsuario] OFF
GO

ALTER TABLE [dbo].[tbUsuario] ADD  CONSTRAINT [UK_CorreoElectronico] UNIQUE NONCLUSTERED 
(
	[CorreoElectronico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbUsuario] ADD  CONSTRAINT [UK_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_tbSolicitud_Admin] FOREIGN KEY([ConsecutivoAdmin])
REFERENCES [dbo].[tbUsuario] ([Consecutivo])
GO
ALTER TABLE [dbo].[tbSolicitud] CHECK CONSTRAINT [FK_tbSolicitud_Admin]
GO

ALTER TABLE [dbo].[tbSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_tbSolicitud_Estado] FOREIGN KEY([ConsecutivoEstado])
REFERENCES [dbo].[tbEstado] ([Consecutivo])
GO
ALTER TABLE [dbo].[tbSolicitud] CHECK CONSTRAINT [FK_tbSolicitud_Estado]
GO

ALTER TABLE [dbo].[tbSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_tbSolicitud_Usuario] FOREIGN KEY([ConsecutivoUsuario])
REFERENCES [dbo].[tbUsuario] ([Consecutivo])
GO
ALTER TABLE [dbo].[tbSolicitud] CHECK CONSTRAINT [FK_tbSolicitud_Usuario]
GO

ALTER TABLE [dbo].[tbUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tbUsuario_tbRol] FOREIGN KEY([ConsecutivoRol])
REFERENCES [dbo].[tbRol] ([Consecutivo])
GO
ALTER TABLE [dbo].[tbUsuario] CHECK CONSTRAINT [FK_tbUsuario_tbRol]
GO

CREATE PROCEDURE [dbo].[spActualizarContrasenna]
    @Consecutivo    int,
    @Contrasenna    varchar(100),
    @IndicadorTemp  bit
AS
BEGIN

    UPDATE  tbUsuario
    SET     Contrasenna = @Contrasenna,
            IndicadorTemp = @IndicadorTemp
    WHERE   Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[spActualizarPerfil]
    @Consecutivo        int,
    @Identificacion     varchar(15),
    @Nombre             varchar(250),
    @CorreoElectronico  varchar(100)
AS
BEGIN

    UPDATE  tbUsuario
    SET     Identificacion      = @Identificacion,
            Nombre              = @Nombre,
            CorreoElectronico   = @CorreoElectronico
    WHERE   Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[spCancelarSolicitudUsuario]
    @ConsecutivoSolicitud   int,
    @ConsecutivoUsuario     int
AS
BEGIN

    UPDATE  tbSolicitud
    SET     ConsecutivoEstado = 3,
            FechaFinalizacion = GETDATE()
    WHERE   Consecutivo        = @ConsecutivoSolicitud
        AND ConsecutivoUsuario = @ConsecutivoUsuario
        AND ConsecutivoEstado  = 1

END
GO

CREATE PROCEDURE [dbo].[spConsultarSolicitud]
    @ConsecutivoSolicitud  int
AS
BEGIN
	
    SELECT S.Consecutivo,
           S.Titulo,
           S.Descripcion,
           S.FechaRegistro,
           S.FechaFinalizacion,
           U.Nombre 'NombreAdmin',
           E.Nombre 'NombreEstado'
      FROM dbo.tbSolicitud S
      INNER JOIN dbo.tbUsuario U ON S.ConsecutivoAdmin = U.Consecutivo
      INNER JOIN dbo.tbEstado E ON S.ConsecutivoEstado = E.Consecutivo
      WHERE S.Consecutivo = @ConsecutivoSolicitud

END
GO

CREATE PROCEDURE [dbo].[spConsultarSolicitudesUsuario]
    @ConsecutivoUsuario  int
AS
BEGIN
	
    SELECT S.Consecutivo,
           S.Titulo,
           S.Descripcion,
           S.FechaRegistro,
           S.FechaFinalizacion,
           U.Nombre 'NombreAdmin',
           E.Nombre 'NombreEstado'
      FROM dbo.tbSolicitud S
      INNER JOIN dbo.tbUsuario U ON S.ConsecutivoAdmin = U.Consecutivo
      INNER JOIN dbo.tbEstado E ON S.ConsecutivoEstado = E.Consecutivo
      WHERE S.ConsecutivoUsuario = @ConsecutivoUsuario

END
GO

CREATE PROCEDURE [dbo].[spConsultarUsuario]
    @Consecutivo  int
AS
BEGIN
	
    SELECT  Consecutivo,
            Identificacion,
            Nombre,
            CorreoElectronico,
            Estado,
            IndicadorTemp
    FROM    dbo.tbUsuario
    WHERE   Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[spIniciarSesionUsuario]
    @CorreoElectronico  varchar(100),
    @Contrasenna        varchar(100)
AS
BEGIN
	
    SELECT  U.Consecutivo,
            Identificacion,
            U.Nombre,
            CorreoElectronico,
            Estado,
            IndicadorTemp,
            Contrasenna,
            ConsecutivoRol,
            R.Nombre 'NombreRol'
    FROM    dbo.tbUsuario U
    INNER JOIN dbo.tbRol R ON U.ConsecutivoRol = R.Consecutivo
    WHERE   CorreoElectronico = @CorreoElectronico
        --AND Contrasenna = @Contrasenna
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

CREATE PROCEDURE [dbo].[spRegistrarSolicitud]
    @Titulo             varchar(150),
    @Descripcion        varchar(max),
    @ConsecutivoUsuario int
AS
BEGIN

    --ADMIN CON MENOS SOLICITUDES EN COLA
    DECLARE @ConsecutivoAdmin INT

    SELECT  TOP 1 @ConsecutivoAdmin = U.Consecutivo
    FROM    dbo.tbUsuario U
    LEFT JOIN dbo.tbSolicitud S ON  S.ConsecutivoAdmin = U.Consecutivo
                                AND S.ConsecutivoEstado = 1
    WHERE   U.ConsecutivoRol = 2
    GROUP BY U.Consecutivo
    ORDER BY COUNT(S.Consecutivo)


    INSERT INTO dbo.tbSolicitud(Titulo,Descripcion,FechaRegistro,FechaFinalizacion,
                                ConsecutivoUsuario,ConsecutivoAdmin,ConsecutivoEstado)
     VALUES(@Titulo,@Descripcion,GETDATE(),NULL,@ConsecutivoUsuario,@ConsecutivoAdmin,1)

END
GO

CREATE PROCEDURE [dbo].[spRegistrarUsuario]
    @Identificacion     varchar(15),
    @Nombre             varchar(250),
    @CorreoElectronico  varchar(100),
    @Contrasenna        varchar(100)
AS
BEGIN

    IF NOT EXISTS (SELECT 1 FROM tbUsuario
                   WHERE Identificacion = @Identificacion
                    OR   CorreoElectronico = @CorreoElectronico)
    BEGIN
	
        DECLARE @Estado BIT = 1
        DECLARE @ClaveTemp BIT = 0
        DECLARE @Rol INT = 1

        INSERT INTO dbo.tbUsuario (Identificacion,Nombre,CorreoElectronico,Contrasenna,Estado,IndicadorTemp,ConsecutivoRol)
        VALUES (@Identificacion,@Nombre,@CorreoElectronico,@Contrasenna,@Estado,@ClaveTemp,@Rol)

    END
END
GO

CREATE PROCEDURE [dbo].[spValidarCorreo]
    @CorreoElectronico  varchar(100)
AS
BEGIN
	
    SELECT  Consecutivo,
            Identificacion,
            Nombre,
            CorreoElectronico,
            Estado,
            IndicadorTemp
    FROM    dbo.tbUsuario
    WHERE   CorreoElectronico = @CorreoElectronico
        AND Estado = 1

END
GO