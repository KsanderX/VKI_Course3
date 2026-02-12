USE [master]
GO

/****** Object:  Database [Shoes] ******/
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Shoes')
DROP DATABASE [Shoes]
GO

CREATE DATABASE [Shoes]
GO

USE [Shoes]
GO

/****** Object:  Table [dbo].[Manufacturer] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Order] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [float] NULL,
	[OrderDate] [datetime] NULL,
	[DeliveryDay] [datetime] NULL,
	[FK_PickUpPoint] [int] NULL,
	[FK_Client] [int] NULL,
	[CodeToReceive] [float] NULL,
	[FK_OrderStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Order_Contents] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Contents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FK_Product] [int] NULL,
	[Quantity] [float] NULL,
	[FK_Order] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[OrderStatus] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PickUpPoint] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PickUpPoint](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Addreses] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Product] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Article] [nvarchar](255) NULL,
	[FK_ProductName] [int] NULL,
	[FK_Unit] [int] NULL,
	[Price] [float] NULL,
	[FK_Supplier] [int] NULL,
	[FK_Manufacturer] [int] NULL,
	[FK_ProductCategory] [int] NULL,
	[Discount] [float] NULL,
	[CountOnStorage] [float] NULL,
	[Description] [nvarchar](255) NULL,
	[Photo] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Product_Name] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Name](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ProductCategory] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Role] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Supplier] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Unit] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[User] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FK_UserRole] [int] NULL,
	[FIO] [nvarchar](255) NULL,
	[Login] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* INSERTS: IDs removed to allow IDENTITY(1,1) to work */

INSERT [dbo].[Manufacturer] ([Name]) VALUES (N'Alessio Nesca')
INSERT [dbo].[Manufacturer] ([Name]) VALUES (N'CROSBY')
INSERT [dbo].[Manufacturer] ([Name]) VALUES (N'Kari')
INSERT [dbo].[Manufacturer] ([Name]) VALUES (N'Marco Tozzi')
INSERT [dbo].[Manufacturer] ([Name]) VALUES (N'Rieker')
INSERT [dbo].[Manufacturer] ([Name]) VALUES (N'Рос')
GO

INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (1, CAST(N'2025-02-27T00:00:00.000' AS DateTime), CAST(N'2025-04-20T00:00:00.000' AS DateTime), 1, 4, 901, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (2, CAST(N'2022-09-28T00:00:00.000' AS DateTime), CAST(N'2025-04-21T00:00:00.000' AS DateTime), 11, 1, 902, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (3, CAST(N'2025-03-21T00:00:00.000' AS DateTime), CAST(N'2025-04-22T00:00:00.000' AS DateTime), 2, 2, 903, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (4, CAST(N'2025-02-20T00:00:00.000' AS DateTime), CAST(N'2025-04-23T00:00:00.000' AS DateTime), 11, 3, 904, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (5, CAST(N'2025-03-17T00:00:00.000' AS DateTime), CAST(N'2025-04-24T00:00:00.000' AS DateTime), 2, 4, 905, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (6, CAST(N'2025-03-01T00:00:00.000' AS DateTime), CAST(N'2025-04-25T00:00:00.000' AS DateTime), 15, 1, 906, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (7, CAST(N'2025-03-01T00:00:00.000' AS DateTime), CAST(N'2025-04-26T00:00:00.000' AS DateTime), 3, 2, 907, 1)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (8, CAST(N'2025-03-31T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime), 19, 3, 908, 2)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (9, CAST(N'2025-04-02T00:00:00.000' AS DateTime), CAST(N'2025-04-28T00:00:00.000' AS DateTime), 5, 4, 909, 2)
INSERT [dbo].[Order] ([OrderNumber], [OrderDate], [DeliveryDay], [FK_PickUpPoint], [FK_Client], [CodeToReceive], [FK_OrderStatus]) VALUES (10, CAST(N'2025-04-03T00:00:00.000' AS DateTime), CAST(N'2025-04-29T00:00:00.000' AS DateTime), 19, 4, 910, 2)
GO

INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (1, 2, 1)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (2, 2, 1)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (3, 1, 2)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (4, 1, 2)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (5, 10, 3)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (6, 10, 3)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (7, 5, 4)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (8, 4, 4)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (1, 2, 5)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (2, 2, 5)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (3, 1, 6)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (4, 1, 6)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (5, 10, 7)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (6, 10, 7)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (7, 5, 8)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (8, 4, 8)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (9, 5, 9)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (10, 1, 9)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (11, 5, 10)
INSERT [dbo].[Order_Contents] ([FK_Product], [Quantity], [FK_Order]) VALUES (12, 5, 10)
GO

INSERT [dbo].[OrderStatus] ([Name]) VALUES (N'Завершен')
INSERT [dbo].[OrderStatus] ([Name]) VALUES (N'Новый ')
INSERT [dbo].[OrderStatus] ([Name]) VALUES (N'В пути')
GO

INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'420151, г. Лесной, ул. Вишневая, 32')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'125061, г. Лесной, ул. Подгорная, 8')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'630370, г. Лесной, ул. Шоссейная, 24')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'400562, г. Лесной, ул. Зеленая, 32')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'614510, г. Лесной, ул. Маяковского, 47')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'410542, г. Лесной, ул. Светлая, 46')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'620839, г. Лесной, ул. Цветочная, 8')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'443890, г. Лесной, ул. Коммунистическая, 1')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'603379, г. Лесной, ул. Спортивная, 46')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'603721, г. Лесной, ул. Гоголя, 41')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'410172, г. Лесной, ул. Северная, 13')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'614611, г. Лесной, ул. Молодежная, 50')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'454311, г.Лесной, ул. Новая, 19')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'660007, г.Лесной, ул. Октябрьская, 19')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'603036, г. Лесной, ул. Садовая, 4')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'394060, г.Лесной, ул. Фрунзе, 43')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'410661, г. Лесной, ул. Школьная, 50')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'625590, г. Лесной, ул. Коммунистическая, 20')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'625683, г. Лесной, ул. 8 Марта')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'450983, г.Лесной, ул. Комсомольская, 26')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'394782, г. Лесной, ул. Чехова, 3')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'603002, г. Лесной, ул. Дзержинского, 28')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'450558, г. Лесной, ул. Набережная, 30')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'344288, г. Лесной, ул. Чехова, 1')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'614164, г.Лесной,  ул. Степная, 30')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'394242, г. Лесной, ул. Коммунистическая, 43')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'660540, г. Лесной, ул. Солнечная, 25')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'125837, г. Лесной, ул. Шоссейная, 40')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'125703, г. Лесной, ул. Партизанская, 49')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'625283, г. Лесной, ул. Победы, 46')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'614753, г. Лесной, ул. Полевая, 35')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'426030, г. Лесной, ул. Маяковского, 44')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'450375, г. Лесной ул. Клубная, 44')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'625560, г. Лесной, ул. Некрасова, 12')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'630201, г. Лесной, ул. Комсомольская, 17')
INSERT [dbo].[PickUpPoint] ([Addreses]) VALUES (N'190949, г. Лесной, ул. Мичурина, 26')
GO

INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'А112Т4', 1, 1, 4990, 1, 3, 1, 3, 6, N'Женские Ботинки демисезонные kari', N'1.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'F635R4', 1, 1, 3244, 2, 4, 1, 2, 13, N'Ботинки Marco Tozzi женские демисезонные, размер 39, цвет бежевый', N'2.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'H782T5', 7, 1, 4499, 1, 3, 2, 4, 5, N'Туфли kari мужские классика MYZ21AW-450A, размер 43, цвет: черный', N'3.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'G783F5', 1, 1, 5900, 1, 6, 2, 2, 8, N'Мужские ботинки Рос-Обувь кожаные с натуральным мехом', N'4.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'J384T6', 1, 1, 3800, 2, 5, 2, 2, 16, N'B3430/14 Полуботинки мужские Rieker', N'5.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'D572U8', 3, 1, 4100, 2, 6, 2, 3, 6, N'129615-4 Кроссовки мужские', N'6.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'F572H7', 7, 1, 2700, 1, 4, 1, 2, 14, N'Туфли Marco Tozzi женские летние, размер 39, цвет черный', N'7.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'D329H3', 4, 1, 1890, 2, 1, 1, 4, 4, N'Полуботинки Alessio Nesca женские 3-30797-47, размер 37, цвет: бордовый', N'8.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'B320R5', 7, 1, 4300, 1, 5, 1, 2, 6, N'Туфли Rieker женские демисезонные, размер 41, цвет коричневый', N'9.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'G432E4', 7, 1, 2800, 1, 3, 1, 3, 15, N'Туфли kari женские TR-YR-413017, размер 37, цвет: черный', N'10.jpg')
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'S213E3', 4, 1, 2156, 2, 2, 2, 3, 6, N'407700/01-01 Полуботинки мужские CROSBY', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'E482R4', 4, 1, 1800, 1, 3, 1, 2, 14, N'Полуботинки kari женские MYZ20S-149, размер 41, цвет: черный', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'S634B5', 2, 1, 5500, 2, 2, 2, 3, 0, N'Кеды Caprice мужские демисезонные, размер 42, цвет черный', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'K345R4', 4, 1, 2100, 2, 2, 2, 2, 3, N'407700/01-02 Полуботинки мужские CROSBY', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'O754F4', 7, 1, 5400, 2, 5, 1, 4, 18, N'Туфли женские демисезонные Rieker артикул 55073-68/37', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'G531F4', 1, 1, 6600, 1, 3, 1, 12, 9, N'Ботинки женские зимние ROMER арт. 893167-01 Черный', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'J542F5', 6, 1, 500, 1, 3, 2, 13, 0, N'Тапочки мужские Арт.70701-55-67син р.41', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'B431R5', 1, 1, 2700, 2, 5, 2, 2, 5, N'Мужские кожаные ботинки/мужские ботинки', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'P764G4', 7, 1, 6800, 1, 2, 1, 15, 15, N'Туфли женские, ARGO, размер 38', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'C436G5', 1, 1, 10200, 1, 1, 1, 15, 9, N'Ботинки женские, ARGO, размер 40', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'F427R5', 1, 1, 11800, 2, 5, 1, 15, 11, N'Ботинки на молнии с декоративной пряжкой FRAU', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'N457T5', 4, 1, 4600, 1, 2, 1, 3, 13, N'Полуботинки Ботинки черные зимние, мех', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'D364R4', 7, 1, 12400, 1, 3, 1, 16, 5, N'Туфли Luiza Belly женские Kate-lazo черные из натуральной замши', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'S326R5', 6, 1, 9900, 2, 2, 2, 17, 15, N'Мужские кожаные тапочки "Профиль С.Дали" ', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'L754R4', 4, 1, 1700, 1, 3, 1, 2, 7, N'Полуботинки kari женские WB2020SS-26, размер 38, цвет: черный', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'M542T5', 3, 1, 2800, 2, 5, 2, 18, 3, N'Кроссовки мужские TOFA', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'D268G5', 7, 1, 4399, 2, 5, 1, 3, 12, N'Туфли Rieker женские демисезонные, размер 36, цвет коричневый', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'T324F5', 5, 1, 4699, 1, 2, 1, 2, 5, N'Сапоги замша Цвет: синий', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'K358H6', 6, 1, 599, 1, 5, 2, 20, 2, N'Тапочки мужские син р.41', NULL)
INSERT [dbo].[Product] ([Article], [FK_ProductName], [FK_Unit], [Price], [FK_Supplier], [FK_Manufacturer], [FK_ProductCategory], [Discount], [CountOnStorage], [Description], [Photo]) VALUES (N'H535R5', 1, 1, 2300, 2, 5, 1, 2, 7, N'Женские Ботинки демисезонные', NULL)
GO

INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Ботинки')
INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Кеды')
INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Кроссовки')
INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Полуботинки')
INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Сапоги')
INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Тапочки')
INSERT [dbo].[Product_Name] ([Name]) VALUES (N'Туфли')
GO

INSERT [dbo].[ProductCategory] ([Name]) VALUES (N'Женская обувь')
INSERT [dbo].[ProductCategory] ([Name]) VALUES (N'Мужская обувь')
GO

INSERT [dbo].[Role] ([Name]) VALUES (N'Администратор')
INSERT [dbo].[Role] ([Name]) VALUES (N'Менеджер')
INSERT [dbo].[Role] ([Name]) VALUES (N'Авторизированный клиент')
GO

INSERT [dbo].[Supplier] ([Name]) VALUES (N'Kari')
INSERT [dbo].[Supplier] ([Name]) VALUES (N'Обувь для вас')
GO

INSERT [dbo].[Unit] ([Name]) VALUES (N'шт.')
GO

INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (1, N'Никифорова Весения Николаевна', N'94d5ous@gmail.com', N'uzWC67')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (1, N'Сазонов Руслан Германович', N'uth4iz@mail.com', N'2L6KZG')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (1, N'Одинцов Серафим Артёмович', N'yzls62@outlook.com', N'JlFRCZ')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (2, N'Степанов Михаил Артёмович', N'1diph5e@tutanota.com', N'8ntwUp')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (2, N'Ворсин Петр Евгеньевич', N'tjde7c@yahoo.com', N'YOyhfR')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (2, N'Старикова Елена Павловна', N'wpmrc3do@tutanota.com', N'RSbvHv')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (3, N'Михайлюк Анна Вячеславовна', N'5d4zbu@tutanota.com', N'rwVDh9')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (3, N'Ситдикова Елена Анатольевна', N'ptec8ym@yahoo.com', N'LdNyos')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (3, N'Ворсин Петр Евгеньевич', N'1qz4kw@mail.com', N'gynQMT')
INSERT [dbo].[User] ([FK_UserRole], [FIO], [Login], [Password]) VALUES (3, N'Старикова Елена Павловна', N'4np6se@mail.com', N'AtnDjr')
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([FK_Client])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([FK_OrderStatus])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([FK_PickUpPoint])
REFERENCES [dbo].[PickUpPoint] ([ID])
GO
ALTER TABLE [dbo].[Order_Contents]  WITH CHECK ADD FOREIGN KEY([FK_Order])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Contents]  WITH CHECK ADD FOREIGN KEY([FK_Product])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([FK_Manufacturer])
REFERENCES [dbo].[Manufacturer] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([FK_ProductName])
REFERENCES [dbo].[Product_Name] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([FK_ProductCategory])
REFERENCES [dbo].[ProductCategory] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([FK_Supplier])
REFERENCES [dbo].[Supplier] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([FK_Unit])
REFERENCES [dbo].[Unit] ([ID])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([FK_UserRole])
REFERENCES [dbo].[Role] ([ID])
GO