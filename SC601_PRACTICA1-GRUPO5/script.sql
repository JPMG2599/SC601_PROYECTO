USE [KN_Proyecto]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([ID_Categoria], [Nombre], [Descripcion], [Imagen], [Activo]) VALUES (1, N'Flores', N'Material principal en arreglos florales', N'/ImagenesCategorias/1.jpg', 1)
INSERT [dbo].[Categoria] ([ID_Categoria], [Nombre], [Descripcion], [Imagen], [Activo]) VALUES (2, N'Amor y amistad', N'Arreglo floral dedicado para amistad o amor', N'/ImagenesCategorias/2.jpg', 1)
INSERT [dbo].[Categoria] ([ID_Categoria], [Nombre], [Descripcion], [Imagen], [Activo]) VALUES (3, N'Arreglos con globos', N'Arreglos que pueden utilizar globos y flores en uno mismo', N'imagen', 1)
INSERT [dbo].[Categoria] ([ID_Categoria], [Nombre], [Descripcion], [Imagen], [Activo]) VALUES (4, N'Tributos funebres', N'Arreglos especiales para velorios o tributos', N'imagen', 1)
INSERT [dbo].[Categoria] ([ID_Categoria], [Nombre], [Descripcion], [Imagen], [Activo]) VALUES (5, N'Cumpleaños', N'Arreglos especializados para cumpleaños', N'imagen', 1)
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Precio], [Imagen], [Activo]) VALUES (17, 1, N'Rosas Rojas', N'Hermosas rosas para regalo romántico', CAST(12000.00 AS Decimal(10, 2)), N'/Imagenes/Productos/17.jpg', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Precio], [Imagen], [Activo]) VALUES (18, 2, N'Girasoles Amarillos', N'Flores alegres para toda ocasión', CAST(9500.00 AS Decimal(10, 2)), N'/Imagenes/Productos/18.jpg', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Precio], [Imagen], [Activo]) VALUES (19, 3, N'Tulipanes Rosados', N'Ramillete de tulipanes frescos', CAST(8700.00 AS Decimal(10, 2)), N'/Imagenes/Productos/19.jpg', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Precio], [Imagen], [Activo]) VALUES (20, 1, N'Orquídea Blanca', N'Elegancia natural para decoración', CAST(15000.00 AS Decimal(10, 2)), N'/Imagenes/Productos/20.jpg', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Precio], [Imagen], [Activo]) VALUES (21, 4, N'Ramo de Claveles', N'Ideal para expresar cariño sincero', CAST(6800.00 AS Decimal(10, 2)), N'/Imagenes/Productos/21.jpg', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Precio], [Imagen], [Activo]) VALUES (22, 2, N'Caja de flores mixtas', N'Surtido especial de flores premium', CAST(18500.00 AS Decimal(10, 2)), N'/Imagenes/Productos/22.jpg', 1)
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([ID_Rol], [Nombre_rol]) VALUES (1, N'Administrador                                     ')
INSERT [dbo].[Rol] ([ID_Rol], [Nombre_rol]) VALUES (2, N'Cliente                                           ')
INSERT [dbo].[Rol] ([ID_Rol], [Nombre_rol]) VALUES (3, N'Vendedor                                          ')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Rol], [Cedula], [Nombre], [Correo], [Contrasena], [Telefono], [Estado]) VALUES (1, 1, N'000000000                                         ', N'Administrador                                                                                       ', N'admin@gmail.com', N'12345678                                                                                            ', N'88888889                                                                                            ', 1)
INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Rol], [Cedula], [Nombre], [Correo], [Contrasena], [Telefono], [Estado]) VALUES (2, 2, N'123456789', N'Usuario Cliente', N'cliente@gmail.com', N'12345678', N'88888888', 1)
INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Rol], [Cedula], [Nombre], [Correo], [Contrasena], [Telefono], [Estado]) VALUES (3, 3, N'987654321', N'Usuario Vendedor', N'vendedor@gmail.com', N'12345678', N'77777777', 1)
INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Rol], [Cedula], [Nombre], [Correo], [Contrasena], [Telefono], [Estado]) VALUES (4, 2, N'123456789', N'Usuario Cliente', N'cliente2@gmail.com', N'12345678', N'88888888', 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Error] ON 

INSERT [dbo].[Error] ([Id], [IdUsuario], [Mensaje], [FechaHora], [Origen]) VALUES (1, 0, N'Intento de dividir por cero.', CAST(N'2025-03-12T16:03:17.070' AS DateTime), N'Get IniciarSesion')
SET IDENTITY_INSERT [dbo].[Error] OFF
GO
