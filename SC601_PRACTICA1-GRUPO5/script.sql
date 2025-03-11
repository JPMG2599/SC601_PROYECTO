USE [KN_Proyecto]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 10/3/2025 19:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID_Categoria] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [char](100) NOT NULL,
	[Descripcion] [char](200) NOT NULL,
	[Imagen] [char](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[ID_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comprador]    Script Date: 10/3/2025 19:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comprador](
	[ID_Comprador] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [bigint] NOT NULL,
	[Nombre] [char](100) NOT NULL,
	[Correo] [char](100) NOT NULL,
	[Telefono] [char](100) NOT NULL,
 CONSTRAINT [PK_Comprador] PRIMARY KEY CLUSTERED 
(
	[ID_Comprador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 10/3/2025 19:13:47 ******/
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
/****** Object:  Table [dbo].[Producto]    Script Date: 10/3/2025 19:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ID_Producto] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Categoria] [bigint] NOT NULL,
	[Nombre] [char](100) NOT NULL,
	[Descripcion] [char](200) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Imagen] [char](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[ID_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 10/3/2025 19:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[ID_Rol] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre_rol] [char](50) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[ID_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/3/2025 19:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID_Usuario] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Rol] [bigint] NOT NULL,
	[Cedula] [char](50) NOT NULL,
	[Nombre] [char](100) NOT NULL,
	[Correo] [char](100) NOT NULL,
	[Contrasena] [char](100) NOT NULL,
	[Telefono] [char](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 10/3/2025 19:13:47 ******/
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
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Comprador] FOREIGN KEY([ID_Comprador])
REFERENCES [dbo].[Comprador] ([ID_Comprador])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Comprador]
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
