USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Final_Project_PRN221')
BEGIN
	ALTER DATABASE [Final_Project_PRN221] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [Final_Project_PRN221] SET ONLINE;
	DROP DATABASE [Final_Project_PRN221];
END
GO
CREATE DATABASE [Final_Project_PRN221]
GO
USE [Final_Project_PRN221]
GO
DECLARE @sql nvarchar(MAX) 
SET @sql = N'' 

SELECT @sql = @sql + N'ALTER TABLE ' + QUOTENAME(KCU1.TABLE_SCHEMA) 
    + N'.' + QUOTENAME(KCU1.TABLE_NAME) 
    + N' DROP CONSTRAINT ' -- + QUOTENAME(rc.CONSTRAINT_SCHEMA)  + N'.'  -- not in MS-SQL
    + QUOTENAME(rc.CONSTRAINT_NAME) + N'; ' + CHAR(13) + CHAR(15) 
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 

INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 
    ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
    AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
    AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 

EXECUTE(@sql) 

GO
DECLARE @sql2 NVARCHAR(max)=''

SELECT @sql2 += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

Exec Sp_executesql @sql2 

/****** Object:  Table [dbo].[account]    Script Date: 9/20/2022 10:23:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Account
(
	Id				INT IDENTITY PRIMARY KEY NOT NULL,
	FullName		VARCHAR(50) NOT NULL,
	[User]			VARCHAR(50) UNIQUE NOT NULL,
	[Password]		VARCHAR(50) NOT NULL,
	[Role]			BIT NULL
) 
GO

CREATE TABLE [Type]
(
	Id				INT PRIMARY KEY NOT NULL,
	Title			VARCHAR(50) NOT NULL,
	PricePerHour	MONEY NOT NULL
)
GO

CREATE TABLE [Table]
(	
	Id				INT PRIMARY KEY NOT NULL,
	[Name]			VARCHAR(50) NOT NULL,
	[Image]			VARCHAR(100) NOT NULL,
	IsOn			BIT DEFAULT(0),
	[TypeId]		INT FOREIGN KEY REFERENCES [Type](Id) NOT NULL
)
GO 

CREATE TABLE Category
(
	Id				INT PRIMARY KEY NOT NULL,
	[Name]			VARCHAR(50) NOT NULL,
)
GO 

CREATE TABLE Product
(
	Id				INT IDENTITY PRIMARY KEY NOT NULL,
	[Name]			VARCHAR(50) NOT NULL,
	UnitPrice		MONEY NOT NULL,
	UnitsInStock	INT NOT NULL,
	[Image]			NVARCHAR(50) DEFAULT('product.png') NULL,
	CategoryId		INT FOREIGN KEY REFERENCES Category(Id) NOT NULL,
)
GO 

CREATE TABLE [Order]
(
	Id				INT IDENTITY PRIMARY KEY NOT NULL,
	TableId			INT FOREIGN KEY REFERENCES [Table](Id) NOT NULL,
	Discount		FLOAT DEFAULT(0) NULL,
	StartTime		DATETIME DEFAULT(GETDATE()) NOT NULL,
	EndTime			DATETIME NULL,
	OrderBy			INT FOREIGN KEY REFERENCES Account(Id) NULL
)
GO

CREATE TABLE OrderDetail
(
	OrderId			INT FOREIGN KEY REFERENCES [Order](Id) NOT NULL,
	ProductId		INT FOREIGN KEY REFERENCES Product(Id) NOT NULL,
	Quantity		INT	NOT NULL,
	PRIMARY KEY (OrderId, ProductId)
)
GO

SET IDENTITY_INSERT Account ON
INSERT INTO Account(Id,FullName,[User],[Password],[Role])
VALUES
(1, 'Vu Hoai Nam', 'admin', 'admin@@@', NULL),
(2, 'Pham Quoc Hung', 'hungpq', '123123', 1),
(3, 'Vu Huu Quang', 'quangvh', '123456', 0),
(4, 'Doan Ngoc Thu', 'thudn', 'thuuu', 1),
(5, 'Nguyen Thien Thanh', 'thanhnt', '666888', 1),
(6, 'Vu Huu Ly', 'Lyvh', '123456', 0),
(7, 'Nguyen Tuan Thanh', 'thanhnt01', '666888', 1),
(8, 'Nguyen Thu Phuong', 'phuongnt', '123456', 1);

SET IDENTITY_INSERT Account OFF
GO

INSERT INTO [Type](Id,Title,PricePerHour)
VALUES
(1, 'Vip table', 100000),
(2, 'Normal table', 80000);
GO

INSERT INTO [Table](Id,[Name],[Image],[TypeId])
VALUES
(1, 'Table 01', 'vipTable.png', 1),
(2, 'Table 02', 'vipTable.png', 1),
(3, 'Table 03', 'vipTable.png', 1),
(4, 'Table 04', 'vipTable.png', 1),
(5, 'Table 05', 'vipTable.png', 1),
(6, 'Table 06', 'table.png', 2),
(7, 'Table 07', 'table.png', 2),
(8, 'Table 08', 'table.png', 2),
(9, 'Table 09', 'table.png', 2),
(10, 'Table 10', 'table.png', 2),
(11, 'Table 11', 'table.png', 2),
(12, 'Table 12', 'table.png', 2),
(13, 'Table 13', 'table.png', 2),
(14, 'Table 14', 'table.png', 2),
(15, 'Table 15', 'table.png', 2);
GO

INSERT INTO Category(Id,[Name])
VALUES
(1, 'Drink'),
(2, 'Food'),
(3, 'Cues'),
(4, 'Other');
GO

SET IDENTITY_INSERT Product ON
INSERT INTO Product(Id,[Name],UnitPrice,UnitsInStock,[Image],CategoryId)
VALUES
(1,'Coca Cola', 15000, 123, 'coca.png', 1),
(2,'Pepsi', 15000, 99, 'pepsi.png', 1),
(3,'Sting', 20000, 111, 'sting.png', 1),
(4,'Rhino', 25000, 100, 'rhino.png', 1),
(5,'Milk tea', 25000, 50, 'milkTea.png', 1),
(6,'Peach tea', 15000, 80, 'peachTea.png', 1),
(7,'Mirinda', 15000, 79, 'mirinda.png', 1),
(8,'Lemon tea', 15000, 68, 'lemonTea.png', 1),
(9,'Beer', 20000, 123, 'beer.png', 1),
(10,'Nutri Boost', 15000, 123, 'nutriBoost.png', 1),
(11,'Ostar Snack', 10000, 97, 'OstarSnack.png', 2),
(12,'Lay Snack', 10000, 123, 'laySnack.png', 2),
(13,'Noodles', 30000, 100, 'noodles.png', 2),
(14,'Sandwich', 25000, 123, 'sandwich.png', 2),
(15,'Hamburger', 15000, 123, 'hamburger.png', 2),
(16,'Fury Cw3', 30000, 5, 'furyCw3.png', 3),
(17,'Fury Cw4', 30000, 5, 'furyCw4.png', 3),
(18,'Fury Carbon', 50000, 2, 'furyCarbon.png', 3),
(19,'K-Alpha', 50000, 2, 'kAlpha.png', 3),
(20,'K-Beta', 50000, 2, 'kBeta.png', 3),
(21,'Thang Long Cigarette', 25000, 200, 'TLCigarette.png', 4),
(22,'Sai Gon Cigarette', 25000, 210, 'SGCigarette.png', 4),
(23,'Wet Wipes', 3000, 150, 'wetWipes.png', 4);
SET IDENTITY_INSERT Product OFF
GO

SET IDENTITY_INSERT [Order] ON
INSERT INTO [Order] (Id, TableId, Discount, StartTime, EndTime, OrderBy)
VALUES
    (1, 1, 20, '2023-06-28 13:21:34', '2023-06-28 16:13:14', 2),
    (2, 2, 0, '2023-06-28 13:31:24', '2023-06-28 19:23:14', 2),
    (3, 3, 30, '2023-06-28 14:11:32', '2023-06-28 19:33:44', 2),
    (4, 4, 0, '2023-06-28 15:21:34', '2023-06-28 18:23:14', 4),
    (5, 5, 0, '2023-06-28 17:21:34', '2023-06-28 20:23:14', 5),
    (6, 1, 0, '2023-06-29 19:31:34', '2023-06-29 22:23:14', 4),
    (7, 2, 0, '2023-06-29 19:21:34', '2023-06-30 00:23:14', 2),
    (8, 3, 10, '2023-06-29 22:21:34', '2023-06-30 02:23:14', 5),
    (9, 4, 0, '2023-06-29 20:21:34', '2023-06-30 04:23:14', 4),
    (10, 5, 0, '2023-06-30 01:21:34', '2023-06-30 05:23:14', 5),
	(11, 1, 20, '2023-06-30 13:21:34', '2023-06-30 16:13:14', 2),
    (12, 2, 0, '2023-06-30 13:31:24', '2023-06-30 19:23:14', 2),
    (13, 3, 30, '2023-06-30 14:11:32', '2023-06-30 19:33:44', 2),
    (14, 4, 0, '2023-06-30 15:21:34', '2023-06-30 18:23:14', 4),
    (15, 5, 0, '2023-06-30 17:21:34', '2023-06-30 20:23:14', 5),
    (16, 1, 0, '2023-07-01 19:31:34', '2023-07-01 22:23:14', 4),
    (17, 2, 0, '2023-07-01 19:21:34', '2023-07-02 00:23:14', 2),
    (18, 3, 10, '2023-07-01 22:21:34', '2023-07-02 02:23:14', 5),
    (19, 4, 0, '2023-07-01 20:21:34', '2023-07-02 04:23:14', 4),
    (20, 5, 0, '2023-07-02 01:21:34', '2023-07-02 05:23:14', 5)
SET IDENTITY_INSERT [Order] OFF
GO

INSERT INTO OrderDetail (OrderId, ProductId, Quantity)
VALUES
    (1, 1, 5),
    (1, 2, 3),
    (2, 1, 2),
    (2, 3, 1),
    (2, 2, 4),
    (3, 3, 2),
    (4, 1, 3),
    (4, 2, 2),
    (4, 3, 1),
    (4, 4, 2),
    (4, 20, 2),
    (4, 21, 2),
    (6, 2, 4),
    (6, 3, 3),
    (6, 1, 2),
    (8, 3, 1),
    (8, 2, 4),
    (8, 4, 2),
    (9, 1, 3),
    (9, 2, 2),
    (9, 3, 1),
    (10, 4, 2);
GO




