USE [KN_Proyecto]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID_Categoria] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[Imagen] [varchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[ID_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comprador]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comprador](
	[ID_Comprador] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [bigint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Telefono] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Comprador] PRIMARY KEY CLUSTERED 
(
	[ID_Comprador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Error]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Error](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NULL,
	[Mensaje] [varchar](max) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[Origen] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Error] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[ID_Factura] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [bigint] NOT NULL,
	[ID_Comprador] [bigint] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[ID_Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ID_Producto] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Categoria] [bigint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Imagen] [varchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[ID_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[ID_Rol] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre_rol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[ID_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID_Usuario] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Rol] [bigint] NOT NULL,
	[Cedula] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Contrasena] [varchar](100) NOT NULL,
	[Telefono] [varchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[ID_Venta] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Factura] [bigint] NOT NULL,
	[ID_Producto] [bigint] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Cantidad] [bigint] NOT NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[ID_Venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Error] ON 

INSERT [dbo].[Error] ([Id], [IdUsuario], [Mensaje], [FechaHora], [Origen]) VALUES (1, 0, N'Intento de dividir por cero.', CAST(N'2025-03-12T16:03:17.070' AS DateTime), N'Get IniciarSesion')
SET IDENTITY_INSERT [dbo].[Error] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([ID_Rol], [Nombre_rol]) VALUES (1, N'Administrador                                     ')
INSERT [dbo].[Rol] ([ID_Rol], [Nombre_rol]) VALUES (2, N'Cliente                                           ')
INSERT [dbo].[Rol] ([ID_Rol], [Nombre_rol]) VALUES (3, N'Vendedor                                          ')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Rol], [Cedula], [Nombre], [Correo], [Contrasena], [Telefono], [Estado]) VALUES (1, 1, N'000000000                                         ', N'Administrador                                                                                       ', N'admin@gmail.com', N'12345678                                                                                            ', N'88888888                                                                                            ', 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Comprador]  WITH CHECK ADD  CONSTRAINT [FK_Comprador_Usuario] FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO
ALTER TABLE [dbo].[Comprador] CHECK CONSTRAINT [FK_Comprador_Usuario]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Comprador] FOREIGN KEY([ID_Comprador])
REFERENCES [dbo].[Comprador] ([ID_Comprador])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Comprador]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Usuario] FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Usuario]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([ID_Categoria])
REFERENCES [dbo].[Categoria] ([ID_Categoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([ID_Rol])
REFERENCES [dbo].[Rol] ([ID_Rol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Factura] FOREIGN KEY([ID_Factura])
REFERENCES [dbo].[Factura] ([ID_Factura])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Factura]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Producto] FOREIGN KEY([ID_Producto])
REFERENCES [dbo].[Producto] ([ID_Producto])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Producto]
GO
/****** Object:  StoredProcedure [dbo].[RegistrarError]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarError]
	@IdUsuario bigint,
	@Mensaje varchar(max),
	@Origen varchar(500)
AS
BEGIN
	INSERT INTO dbo.Error (
			IdUsuario, 
			Mensaje, 
			FechaHora, 
			Origen)
	VALUES (@IdUsuario, 
			@Mensaje,
			GETDATE(),
			@Origen)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ActualizarContrasena]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ActualizarContrasena]
	@Correo char(100),
	@ContrasenaActual varchar(100),
	@ContrasenaNueva varchar(100)
AS
BEGIN
	-- ID del usuario que cambiará la contraseña
	DECLARE @ID_Usuario BIGINT;

	-- Buscamos el usuario cuyo correo y contraseñan coicidan con los enviados por el usuario
	SELECT @ID_Usuario = Id_Usuario
	FROM dbo.Usuario
	WHERE Correo = @Correo
		AND Contrasena = @ContrasenaActual

	-- Si el usuario existe, actualiza la contraseña
	IF @ID_Usuario IS NOT NULL
	BEGIN
		UPDATE dbo.Usuario
		SET Contrasena = @ContrasenaNueva
		WHERE ID_Usuario = @ID_Usuario
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ActualizarUsuario]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ActualizarUsuario]
	@ID_Usuario bigint,
	@Id_Rol bigint,
	@Cedula varchar(50),
	@Nombre varchar(100),
	@Correo varchar(100),
	@Telefono varchar(100),
	@Estado bit
AS
BEGIN
	UPDATE Usuario
	SET Id_Rol = @Id_Rol,
		Cedula = @Cedula,
		Nombre = @Nombre,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
	WHERE ID_Usuario = @ID_Usuario;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_IniciarSesion]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_IniciarSesion]
	@Correo varchar(100),
	@Contrasena varchar(100)
AS
BEGIN
	SELECT U.ID_Usuario,
		   R.Nombre_rol,
		   U.Contrasena,
		   U.Correo,
		   U.Nombre
	FROM dbo.Usuario U
	INNER JOIN dbo.Rol R ON U.ID_Rol = R.ID_Rol

	WHERE U.Correo = @Correo
		AND Contrasena = @Contrasena
		AND Estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ListarRoles]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ListarRoles]
AS
BEGIN
	SELECT ID_Rol,
		   Nombre_rol
	FROM Rol;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ListarUsuarios]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ListarUsuarios] 
AS
BEGIN
	SELECT U.Id_Usuario,
		   U.Nombre,
		   U.Cedula,
		   U.Correo,
		   R.Nombre_rol
	FROM dbo.Usuario U
	INNER JOIN dbo.Rol R ON U.ID_Rol = R.ID_Rol
	WHERE Estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RegistrarUsuario]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RegistrarUsuario] 
	@Cedula varchar(50),
	@Nombre varchar(100),
	@Correo varchar(100),
	@Contrasena varchar(100),
	@Telefono varchar(100)
AS
BEGIN
	-- Si la cédula y correo no han sido registrados, se crea el usuario
	IF NOT EXISTS(SELECT 1
				  FROM dbo.Usuario
				  WHERE @Cedula = Cedula
					OR @Correo = Correo)
	BEGIN
		-- Por defecto, el rol es cliente (2) y el estado es activo (1)
		INSERT INTO dbo.Usuario (ID_Rol, Cedula, Nombre, Correo, Contrasena, Telefono, Estado)
		VALUES (2, @Cedula, @Nombre, @Correo, @Contrasena, @Telefono, 1)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ResetearContrasena]    Script Date: 12/3/2025 16:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ResetearContrasena]
	@Correo varchar(100)
AS
BEGIN
	DECLARE @Password char(100)

	-- Si el correo existe, generamos un contraseña aleatoria
	IF EXISTS (SELECT 1
			   FROM dbo.Usuario
			   WHERE Correo = @Correo
				AND Estado = 1)
	BEGIN
		-- Generar una contraseña aleatoria
        SET @Password = LEFT(NEWID(), 10); 

		UPDATE dbo.Usuario
        SET Contrasena = @Password
        WHERE Correo = @Correo;

		SELECT ID_Usuario, Nombre, Correo, @Password AS NuevaContrasena
		FROM dbo.Usuario
		WHERE Correo = @Correo 
			AND Estado = 1
	END
	ELSE
    BEGIN
        -- Devolver NULL si el correo no existe
        SELECT NULL AS NuevaContrasena;
    END
END
GO
