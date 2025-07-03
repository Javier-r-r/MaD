USE [Mad201]
GO

-- ========================
-- Categorías Padre
-- ========================
INSERT INTO [dbo].[CategorySet] ( [categoryName], [Category2_Id]) VALUES 
(N'Comida Rápida', NULL),
(N'Italiana', NULL),
(N'Japonesa', NULL),
(N'Postres', NULL);
GO

-- ========================
-- Subcategorías de Comida Rápida
-- ========================
INSERT INTO [dbo].[CategorySet] ([categoryName], [Category2_Id]) VALUES 
(N'Hamburguesas', 1),
(N'Patatas Fritas', 1),
(N'Pollo Frito', 1);
GO

-- ========================
-- Subcategorías de Italiana
-- ========================
INSERT INTO [dbo].[CategorySet] ([categoryName], [Category2_Id]) VALUES 
(N'Pizzas', 2),
(N'Pastas', 2),
(N'Risottos', 2);
GO

-- ========================
-- Subcategorías de Japonesa
-- ========================
INSERT INTO [dbo].[CategorySet] ([categoryName], [Category2_Id]) VALUES 
(N'Sushi', 3),
(N'Ramen', 3),
(N'Tempura', 3);
GO

-- ========================
-- Subcategorías de Postres
-- ========================
INSERT INTO [dbo].[CategorySet] ([categoryName], [Category2_Id]) VALUES 
(N'Helados', 4),
(N'Tartas', 4),
(N'Frutas', 4);
GO

-- ========================
-- Propiedades específicas
-- ========================
INSERT INTO [dbo].[PropertySet] ([name]) VALUES 
(N'Alérgenos'),
(N'Apto para veganos'),
(N'Picante'),
(N'Contiene gluten'),
(N'Contiene lactosa'),
(N'Capacidad (cl)'),
(N'Contiene frutos secos'),
(N'Calorías'),
(N'Peso neto (g)'),
(N'Producto ecológico');
GO

-- ==================================
-- Usuarios restaurante y cliente
-- ==================================
INSERT INTO [dbo].[UserSet]
           ([login], [password], [name], [address], [email], [language], [country])
VALUES
           (N'McDonalds', N'ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=', N'McDonalds', N'Calle Gran Vía 45, Madrid', N'info@mcdonalds.com', N'es', N'ES'),
           (N'LaTrattoria', N'ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=', N'LaTrattoria', N'Via Roma 12, Roma', N'contact@trattoria.it', N'es', N'ES'),
           (N'SushiZen', N'ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=', N'SushiZen', N'Shibuya 34, Tokio', N'sushi@zen.jp', N'es', N'ES'),
           (N'SweetPalace', N'ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=', N'SweetPalace', N'Rue de Paris 99, París', N'sweet@palace.fr', N'es', N'ES'),
		   (N'carlos', N'ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=', N'Carlos', N'Calle Mayor 87, Madrid', N'carlos.mendez@example.com', N'es', N'ES'),
           (N'marta', N'ypeBEsobvcr6wjGzmiPcTaeG7/gUfE5yuYB3ha/uSLs=', N'Marta', N'Avenida de América 23, Madrid', N'marta.garcia@example.com', N'es', N'ES');
GO

-- ==================================
-- Propiedades de restaurantes
-- ==================================

INSERT INTO [dbo].[UserSet_Restaurant]
           ([schedule], [type], [Id])
VALUES
           (N'L-D 10:00-23:00', N'Comida rápida', 1),
           (N'L-D 12:00-22:30', N'Italiana', 2),
           (N'L-D 11:30-21:30', N'Japonesa', 3),
           (N'L-D 09:00-20:00', N'Postres', 4);
GO

-- ==================================
-- Productos restaurantes
-- ==================================

-- Productos de McDonalds 
INSERT INTO [dbo].[ProductSet] ([name], [price], [creationDate], [stock], [Category_Id], [Restaurant_Id]) VALUES
	('Big Mac', 5.50, '2025-05-12', 100, 5, 1),
	('McNuggets (10 piezas)', 4.20, '2025-05-12', 80, 7, 1),
	('Patatas Grandes', 2.80, '2025-05-12', 120, 6, 1);

-- Productos de LaTrattoria
INSERT INTO [dbo].[ProductSet] ([name], [price], [creationDate], [stock], [Category_Id], [Restaurant_Id]) VALUES
	('Pizza Margarita', 8.50, '2025-05-12', 50, 8, 2),
	('Spaghetti Carbonara', 9.00, '2025-05-12', 45, 9, 2),
	('Risotto de Setas', 10.50, '2025-05-12', 30, 10, 2);

-- Productos de SushiZen
INSERT INTO [dbo].[ProductSet] ([name], [price], [creationDate], [stock], [Category_Id], [Restaurant_Id]) VALUES
	('Sushi Variado (12 piezas)', 12.00, '2025-05-12', 40, 11, 3),
	('Ramen de Cerdo', 9.50, '2025-05-12', 35, 12, 3),
	('Tempura Mixta', 8.75, '2025-05-12', 25, 13, 3);

-- Productos de SweetPalace
INSERT INTO [dbo].[ProductSet] ([name], [price], [creationDate], [stock], [Category_Id], [Restaurant_Id]) VALUES
	('Helado de Vainilla', 3.00, '2025-05-12', 60, 14, 4),
	('Tarta de Queso', 4.50, '2025-05-12', 40, 15, 4),
	('Brocheta de Frutas', 3.80, '2025-05-12', 30, 16, 4);
GO

-- ==================================
-- Propiedades de productos 
-- ==================================
INSERT INTO [dbo].[ProductPropertySet] ([value], [Product_Id], [Property_Id]) VALUES
	('Sí', 1, 1),
	('550 kcal', 1, 8),
	('Sí', 2, 7),
	('Sí', 2, 2),
	('Huevo, Soja, Gluten', 2, 1),
	('No', 4, 10),
	('550 kcal', 4, 8),
	('Sí', 5, 4),
	('Sí', 5, 7),
	('Moderado', 6, 3),
	('250g', 6, 9),
	('Sí', 6, 1),
	('Sí', 7, 5),
	('Sí', 7, 6),
	('No', 8, 10),
	('Sí', 9, 1),
	('Sí', 9, 4),
	('Sí', 9, 7),
	('Sí', 10, 2),
	('Moderado', 10, 3),
	('250g', 10, 9),
	('Sí', 11, 5),
	('Sí', 11, 6)
GO



-- ==================================
-- Propiedades de clientes
-- ==================================
INSERT INTO [dbo].[UserSet_Client]
           ([surname], [Id])
     VALUES
           ('Mendez Rodrígez' , 5),
		   ('García García' , 6);
GO

-- ==================================
-- Tarjetas de clientes
-- ==================================
INSERT INTO [dbo].[BankcardSet]
           ([number], [cardtype], [cvv], [expirationdate], [default], [name], [Client_Id])
     VALUES 
           (1234567890123456, 'Credit', 123, '20251231', 1, 'Carlos Mendez', 5),
           (2345678901234567, 'Debit', 456, '20260531', 0, 'Ana Mendez', 5),
           (3456789012345678, 'Credit', 789, '20241130', 1, 'Marta García', 6),
           (4567890123456789, 'Debit', 321, '20250815', 0, 'Laura García', 6);
GO


