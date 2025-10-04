USE [master]
GO

CREATE DATABASE [BD_SM]
GO

USE [BD_SM]
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

SET IDENTITY_INSERT [dbo].[tbPerfil] ON 
GO
INSERT [dbo].[tbPerfil] ([ConsecutivoPerfil], [Nombre]) VALUES (1, N'Usuario Administrador')
GO
INSERT [dbo].[tbPerfil] ([ConsecutivoPerfil], [Nombre]) VALUES (2, N'Usuario Regular')
GO
SET IDENTITY_INSERT [dbo].[tbPerfil] OFF
GO

SET IDENTITY_INSERT [dbo].[tbUsuario] ON 
GO
INSERT [dbo].[tbUsuario] ([ConsecutivoUsuario], [Identificacion], [Nombre], [CorreoElectronico], [Contrasenna], [Estado], [ConsecutivoPerfil]) VALUES (1, N'208690734', N'DIEGO ARMANDO PICADO MURILLO', N'dpicado90734@ufide.ac.cr', N'90734', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[tbUsuario] OFF
GO

ALTER TABLE [dbo].[tbUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tbUsuario_tbPerfil] FOREIGN KEY([ConsecutivoPerfil])
REFERENCES [dbo].[tbPerfil] ([ConsecutivoPerfil])
GO
ALTER TABLE [dbo].[tbUsuario] CHECK CONSTRAINT [FK_tbUsuario_tbPerfil]
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
