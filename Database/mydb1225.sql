USE [master]
GO
/****** Object:  Database [LPS-Database]    Script Date: 2019/12/25 21:37:03 ******/
CREATE DATABASE [LPS-Database]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LPS-Dataset', FILENAME = N'E:\Program\SQLServer2017\MSSQL14.SQLEXPRESS\MSSQL\DATA\LPS-Dataset.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LPS-Dataset_log', FILENAME = N'E:\Program\SQLServer2017\MSSQL14.SQLEXPRESS\MSSQL\DATA\LPS-Dataset_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LPS-Database] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LPS-Database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LPS-Database] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LPS-Database] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LPS-Database] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LPS-Database] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LPS-Database] SET ARITHABORT OFF 
GO
ALTER DATABASE [LPS-Database] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LPS-Database] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LPS-Database] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LPS-Database] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LPS-Database] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LPS-Database] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LPS-Database] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LPS-Database] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LPS-Database] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LPS-Database] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LPS-Database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LPS-Database] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LPS-Database] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LPS-Database] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LPS-Database] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LPS-Database] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LPS-Database] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LPS-Database] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LPS-Database] SET  MULTI_USER 
GO
ALTER DATABASE [LPS-Database] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LPS-Database] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LPS-Database] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LPS-Database] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LPS-Database] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LPS-Database] SET QUERY_STORE = OFF
GO
USE [LPS-Database]
GO
/****** Object:  User [Colin]    Script Date: 2019/12/25 21:37:03 ******/
CREATE USER [Colin] FOR LOGIN [Colin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Admin_information]    Script Date: 2019/12/25 21:37:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin_information](
	[Admin_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Admin_name] [char](15) NOT NULL,
	[Admin_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Admin_information] PRIMARY KEY CLUSTERED 
(
	[Admin_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Backorder_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Backorder_information](
	[Backorder_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Product_id] [int] NOT NULL,
	[Num] [int] NOT NULL,
	[isHandled] [bit] NOT NULL,
 CONSTRAINT [PK_Backorder_information] PRIMARY KEY CLUSTERED 
(
	[Backorder_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_information](
	[Customer_id_PK] [char](7) NOT NULL,
	[College_name] [char](30) NOT NULL,
	[Customer_contact] [char](15) NOT NULL,
	[Customer_contact_phone] [char](50) NULL,
	[Customer_contact_email] [char](30) NULL,
	[Customer_password] [char](30) NOT NULL,
 CONSTRAINT [PK_class_information] PRIMARY KEY CLUSTERED 
(
	[Customer_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory_information](
	[Inventory_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Product_id] [int] NOT NULL,
	[Num] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Place] [char](50) NOT NULL,
 CONSTRAINT [PK_Inventory_information] PRIMARY KEY CLUSTERED 
(
	[Inventory_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_form]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_form](
	[Order_form_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Customer_id_FK] [char](7) NOT NULL,
	[Create_date] [datetime] NOT NULL,
	[Order_form_status] [char](10) NOT NULL,
	[Order_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Order_form] PRIMARY KEY CLUSTERED 
(
	[Order_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_information](
	[Order_Information_form_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Order_form_id_FK] [int] NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Order_Information_form] PRIMARY KEY CLUSTERED 
(
	[Order_Information_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_to_supplier]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_to_supplier](
	[Order_form_to_supplier_id_PK] [char](6) NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	[Price_of_all] [float] NOT NULL,
	[Order_form_to_supplier_createdate] [datetime] NOT NULL,
	[Order_form_to_supplier_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Order_form_to_supplier] PRIMARY KEY CLUSTERED 
(
	[Order_form_to_supplier_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_to_supplier_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_to_supplier_information](
	[Order_form_to_supplier_information_id_PK] [char](6) NOT NULL,
	[Order_form_to_supplier_id_FK] [char](6) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_model] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Price_of_product] [float] NOT NULL,
 CONSTRAINT [PK_Order_form_to_supplier_information] PRIMARY KEY CLUSTERED 
(
	[Order_form_to_supplier_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pick_up_order]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pick_up_order](
	[Pick_up_order_id_PK] [char](6) NOT NULL,
	[Customer_id_FK] [char](7) NOT NULL,
	[Pick_up_order_createdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Pick_up_order] PRIMARY KEY CLUSTERED 
(
	[Pick_up_order_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pick_up_order_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pick_up_order_information](
	[Pick_up_order_informatin_id_PK] [char](6) NOT NULL,
	[Pick_up_order_id_FK] [char](6) NOT NULL,
	[Product_id_FK] [int] NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_model] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
 CONSTRAINT [PK_Pick_up_order_information] PRIMARY KEY CLUSTERED 
(
	[Pick_up_order_informatin_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_information](
	[Product_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
 CONSTRAINT [PK_Product_information] PRIMARY KEY CLUSTERED 
(
	[Product_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Purchase_form_id_PK] [char](6) NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	[Purchase_form_createdate] [datetime] NOT NULL,
	[Price_of_all] [float] NOT NULL,
	[Purchase_form_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Purchase_form] PRIMARY KEY CLUSTERED 
(
	[Purchase_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase_information](
	[Purchase_form_information_id_PK] [char](6) NOT NULL,
	[Product_id_FK] [int] NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_model] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Price_of_product] [float] NOT NULL,
 CONSTRAINT [PK_Purchase_form_information] PRIMARY KEY CLUSTERED 
(
	[Purchase_form_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation](
	[Quotation_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Quotation_source] [char](8) NOT NULL,
	[Quotation_createdate] [datetime] NOT NULL,
	[Quotation_notes] [char](70) NOT NULL,
	[RFQ_id_FK] [int] NOT NULL,
	[Handler_id_FK] [int] NOT NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED 
(
	[Quotation_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation_information](
	[Quotation_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Quotation_id_FK] [int] NOT NULL,
	[RFQ_information_id_FK] [int] NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_Quotation_information] PRIMARY KEY CLUSTERED 
(
	[Quotation_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RFQ]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RFQ](
	[RFQ_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[RFQ_createdate] [datetime] NOT NULL,
	[RFQ_status] [char](6) NOT NULL,
	[RFQ_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_RFQ] PRIMARY KEY CLUSTERED 
(
	[RFQ_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RFQ_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RFQ_information](
	[RFQ_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[RFQ_id_FK] [int] NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
 CONSTRAINT [PK_RFQ_information] PRIMARY KEY CLUSTERED 
(
	[RFQ_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales_batch]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_batch](
	[Sales_batch_id_PK] [char](7) NOT NULL,
	[Customer_id_FK] [char](7) NOT NULL,
	[Order_form_id_FK] [int] NOT NULL,
	[Source_of_goods] [char](6) NOT NULL,
	[Admin_id_FK] [int] NOT NULL,
	[Price_of_all] [float] NOT NULL,
	[createdate] [datetime] NOT NULL,
	[Sales_batch_status] [char](6) NOT NULL,
	[Sales_batch_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Sales_batch] PRIMARY KEY CLUSTERED 
(
	[Sales_batch_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales_order]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_order](
	[Sales_order_id_PK] [char](6) NOT NULL,
	[Sales_batch_id_FK] [char](7) NOT NULL,
	[Product_id_FK] [int] NOT NULL,
	[Price_of_product] [float] NOT NULL,
	[Sales_order_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Sales_order] PRIMARY KEY CLUSTERED 
(
	[Sales_order_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[School_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[School_information](
	[School_id] [char](6) NOT NULL,
	[School_name] [nchar](30) NOT NULL,
 CONSTRAINT [PK_School_information] PRIMARY KEY CLUSTERED 
(
	[School_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier_information]    Script Date: 2019/12/25 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_information](
	[Supplier_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Supplier_name] [char](20) NOT NULL,
	[Supplier_phone] [char](50) NULL,
	[Supplier_email] [char](30) NULL,
	[Supplier_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Supplier_information] PRIMARY KEY CLUSTERED 
(
	[Supplier_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin_information] ON 

INSERT [dbo].[Admin_information] ([Admin_id_PK], [Admin_name], [Admin_password]) VALUES (0, N'费尔奇         ', N'0000                          ')
SET IDENTITY_INSERT [dbo].[Admin_information] OFF
SET IDENTITY_INSERT [dbo].[Backorder_information] ON 

INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (1, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (2, 4, 30, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (3, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (4, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (5, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (6, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (7, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (8, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (9, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (10, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (11, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (12, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (13, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (14, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (15, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (16, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (17, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (18, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (19, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (20, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (21, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (22, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (23, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (24, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (25, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (26, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (27, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (28, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (29, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (30, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (31, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (32, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (33, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (34, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (35, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (36, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (37, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (38, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (39, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (40, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (41, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (42, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (43, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (44, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (45, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (46, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (47, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (48, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (49, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (50, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (51, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (52, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (53, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (54, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (55, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (56, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (57, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (58, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (59, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (60, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (61, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (62, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (63, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (64, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (65, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (66, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (67, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (68, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (69, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (70, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (71, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (72, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (73, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (74, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (75, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (76, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (77, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (78, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (79, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (80, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (81, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (82, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (83, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (84, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (85, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (86, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (87, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (88, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (89, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (90, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (91, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (92, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (93, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (94, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (95, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (96, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (97, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (98, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (99, 3, 1, 0)
GO
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (100, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (101, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (102, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (103, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (104, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (105, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (106, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (107, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (108, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (109, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (110, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (111, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (112, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (113, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (114, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (115, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (116, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (117, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (118, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (119, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (120, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (121, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (122, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (123, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (124, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (125, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (126, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (127, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (128, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (129, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (130, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (131, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (132, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (133, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (134, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (135, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (136, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (137, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (138, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (139, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (140, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (141, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (142, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (143, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (144, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (145, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (146, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (147, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (148, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (149, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (150, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (151, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (152, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (153, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (154, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (155, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (156, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (157, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (158, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (159, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (160, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (161, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (162, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (163, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (164, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (165, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (166, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (167, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (168, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (169, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (170, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (171, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (172, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (173, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (174, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (175, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (176, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (177, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (178, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (179, 3, 1, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (180, 4, 160, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (181, 5, 15, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (182, 6, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (183, 7, 5, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (184, 8, 20, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (185, 9, 50, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (186, 10, 3, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (187, 11, 70, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (188, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (189, 4, 60, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (190, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (191, 11, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (192, 4, 60, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (193, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (194, 11, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (195, 4, 60, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (196, 12, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (197, 11, 10, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (198, 4, 60, 0)
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (199, 12, 10, 0)
GO
INSERT [dbo].[Backorder_information] ([Backorder_information_id_PK], [Product_id], [Num], [isHandled]) VALUES (200, 11, 10, 0)
SET IDENTITY_INSERT [dbo].[Backorder_information] OFF
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'000000 ', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'000000                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001601 ', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'001601                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001602 ', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'001602                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001603 ', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'001603                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001604 ', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'001604                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001605 ', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'001605                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'0031701', N'美术学院                      ', N'梵高           ', NULL, NULL, N'0031701                       ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'0123123', N'计算机学院                    ', N'未填写         ', NULL, NULL, N'0123123                       ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031601', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031601                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031602', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031602                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031603', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031603                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031604', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031604                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031605', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031605                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031606', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031606                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031607', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031607                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'1031608', N'美术学院                      ', N'‘未命名’     ', NULL, NULL, N'031608                        ')
SET IDENTITY_INSERT [dbo].[Order_form] ON 

INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (8, N'0123123', CAST(N'2019-12-05T11:06:01.513' AS DateTime), N'审核不通过', N'test                                                                  ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (9, N'0123123', CAST(N'2019-12-05T11:08:35.157' AS DateTime), N'未审核    ', N'test2                                                                 ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (10, N'0123123', CAST(N'2019-12-05T11:10:27.560' AS DateTime), N'审核通过  ', N'显卡要有十块的                                                        ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (11, N'0123123', CAST(N'2019-12-05T11:15:25.780' AS DateTime), N'未审核    ', N'笔记本要硬皮精装的                                                    ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (12, N'0123123', CAST(N'2019-12-06T09:10:58.603' AS DateTime), N'未审核    ', N'台灯要高级一点的。                                                    ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (13, N'0123123', CAST(N'2019-12-09T16:46:24.337' AS DateTime), N'审核通过  ', N'                                                                      ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (14, N'0123123', CAST(N'2019-12-09T16:47:28.427' AS DateTime), N'审核通过  ', N'                                                                      ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (15, N'0123123', CAST(N'2019-12-09T22:38:13.420' AS DateTime), N'审核通过  ', N'                                                                      ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (16, N'0123123', CAST(N'2019-12-09T22:38:46.817' AS DateTime), N'审核通过  ', N'                                                                      ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (17, N'0123123', CAST(N'2019-12-11T11:27:24.253' AS DateTime), N'审核通过  ', N'20191211test1                                                         ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (18, N'0123123', CAST(N'2019-12-11T11:27:59.313' AS DateTime), N'审核通过  ', N'20191211test2                                                         ')
SET IDENTITY_INSERT [dbo].[Order_form] OFF
SET IDENTITY_INSERT [dbo].[Order_information] ON 

INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (1, 10, N'实验器材类  ', N'深度学习服务器                ', N'GTX1050                       ', 1, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (2, 11, N'办公用品类  ', N'签字笔                        ', N'黑1.0                         ', 50, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (3, 11, N'办公用品类  ', N'笔记本                        ', N'A5                            ', 30, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (4, 11, N'办公用品类  ', N'印泥                          ', N'红                            ', 5, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (5, 12, N'家具类      ', N'电脑椅                        ', N'1.2m                          ', 10, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (6, 12, N'家具类      ', N'台灯                          ', N'20w                           ', 10, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (7, 12, N'家具类      ', N'沙发                          ', N'皮质                          ', 2, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (8, 12, N'家具类      ', N'书架                          ', N'1.5m*1.8m                     ', 2, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (9, 13, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 30, 2)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (10, 13, N'实验器材类  ', N'烧杯                          ', N'300ml                         ', 15, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (11, 13, N'实验器材类  ', N'滤纸                          ', N'纸质                          ', 50, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (12, 13, N'实验器材类  ', N'搅拌棒                        ', N'玻璃                          ', 5, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (13, 14, N'办公用品类  ', N'笔记本                        ', N'A3                            ', 20, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (14, 14, N'办公用品类  ', N'签字笔                        ', N'0.5mm                         ', 50, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (15, 14, N'办公用品类  ', N'笔筒                          ', N'其他                          ', 3, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (16, 15, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 50, 2)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (17, 15, N'办公用品类  ', N'复印纸                        ', N'A5                            ', 50, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (18, 15, N'办公用品类  ', N'复印纸                        ', N'A3                            ', 10, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (19, 16, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 80, 2)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (20, 16, N'办公用品类  ', N'复印纸                        ', N'A5                            ', 20, 0)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (21, 17, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 10, 2)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (22, 17, N'办公用品类  ', N'复印纸                        ', N'A3                            ', 10, 1)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (23, 18, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 50, 2)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product], [Status]) VALUES (24, 18, N'办公用品类  ', N'复印纸                        ', N'A5                            ', 10, 1)
SET IDENTITY_INSERT [dbo].[Order_information] OFF
SET IDENTITY_INSERT [dbo].[Product_information] ON 

INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (3, N'实验器材类  ', N'深度学习服务器                ', N'GTX1050                       ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (4, N'办公用品类  ', N'复印纸                        ', N'A4                            ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (5, N'实验器材类  ', N'烧杯                          ', N'300ml                         ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (6, N'实验器材类  ', N'滤纸                          ', N'纸质                          ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (7, N'实验器材类  ', N'搅拌棒                        ', N'玻璃                          ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (8, N'办公用品类  ', N'笔记本                        ', N'A3                            ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (9, N'办公用品类  ', N'签字笔                        ', N'0.5mm                         ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (10, N'办公用品类  ', N'笔筒                          ', N'其他                          ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (11, N'办公用品类  ', N'复印纸                        ', N'A5                            ')
INSERT [dbo].[Product_information] ([Product_id_PK], [Product_category], [Product_name], [Product_modle]) VALUES (12, N'办公用品类  ', N'复印纸                        ', N'A3                            ')
SET IDENTITY_INSERT [dbo].[Product_information] OFF
SET IDENTITY_INSERT [dbo].[RFQ] ON 

INSERT [dbo].[RFQ] ([RFQ_id_PK], [RFQ_createdate], [RFQ_status], [RFQ_notes]) VALUES (1, CAST(N'2019-12-11T10:18:04.337' AS DateTime), N'等待  ', N'复印纸大批发                                                          ')
INSERT [dbo].[RFQ] ([RFQ_id_PK], [RFQ_createdate], [RFQ_status], [RFQ_notes]) VALUES (2, CAST(N'2019-12-11T10:22:32.823' AS DateTime), N'等待  ', N'复印纸大批发                                                          ')
INSERT [dbo].[RFQ] ([RFQ_id_PK], [RFQ_createdate], [RFQ_status], [RFQ_notes]) VALUES (3, CAST(N'2019-12-11T10:24:48.960' AS DateTime), N'等待  ', N'复印纸大批发                                                          ')
INSERT [dbo].[RFQ] ([RFQ_id_PK], [RFQ_createdate], [RFQ_status], [RFQ_notes]) VALUES (4, CAST(N'2019-12-11T10:26:33.110' AS DateTime), N'等待  ', N'复印纸大批发                                                          ')
INSERT [dbo].[RFQ] ([RFQ_id_PK], [RFQ_createdate], [RFQ_status], [RFQ_notes]) VALUES (5, CAST(N'2019-12-11T10:30:50.650' AS DateTime), N'等待  ', N'复印纸批发啦                                                          ')
INSERT [dbo].[RFQ] ([RFQ_id_PK], [RFQ_createdate], [RFQ_status], [RFQ_notes]) VALUES (9, CAST(N'2019-12-11T14:37:22.010' AS DateTime), N'等待  ', N'                                                                      ')
SET IDENTITY_INSERT [dbo].[RFQ] OFF
SET IDENTITY_INSERT [dbo].[RFQ_information] ON 

INSERT [dbo].[RFQ_information] ([RFQ_information_id_PK], [RFQ_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (2, 4, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 160)
INSERT [dbo].[RFQ_information] ([RFQ_information_id_PK], [RFQ_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (4, 5, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 160)
INSERT [dbo].[RFQ_information] ([RFQ_information_id_PK], [RFQ_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (5, 5, N'办公用品类  ', N'复印纸                        ', N'A5                            ', 70)
INSERT [dbo].[RFQ_information] ([RFQ_information_id_PK], [RFQ_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (6, 5, N'办公用品类  ', N'复印纸                        ', N'A3                            ', 10)
INSERT [dbo].[RFQ_information] ([RFQ_information_id_PK], [RFQ_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (10, 9, N'办公用品类  ', N'复印纸                        ', N'A4                            ', 60)
SET IDENTITY_INSERT [dbo].[RFQ_information] OFF
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'00    ', N'计算机学院                         ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'01    ', N'数学科学学院                        ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'02    ', N'教育信息技术学院                      ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'03    ', N'美术学院                          ')
SET IDENTITY_INSERT [dbo].[Supplier_information] ON 

INSERT [dbo].[Supplier_information] ([Supplier_id_PK], [Supplier_name], [Supplier_phone], [Supplier_email], [Supplier_password]) VALUES (1, N'京东小哥1           ', NULL, NULL, N'000001                        ')
SET IDENTITY_INSERT [dbo].[Supplier_information] OFF
ALTER TABLE [dbo].[Customer_information] ADD  CONSTRAINT [DF_Customer_information_Customer_contact]  DEFAULT ('‘未命名’') FOR [Customer_contact]
GO
ALTER TABLE [dbo].[Order_form] ADD  CONSTRAINT [DF_Order_Order_form_status]  DEFAULT ('未审核') FOR [Order_form_status]
GO
ALTER TABLE [dbo].[Order_information] ADD  CONSTRAINT [DF_Order_information_hasQuery]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Backorder_information]  WITH CHECK ADD  CONSTRAINT [FK_Backorder_information_Product_information] FOREIGN KEY([Product_id])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Backorder_information] CHECK CONSTRAINT [FK_Backorder_information_Product_information]
GO
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_information_Product_information] FOREIGN KEY([Product_id])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [FK_Inventory_information_Product_information]
GO
ALTER TABLE [dbo].[Order_information]  WITH CHECK ADD  CONSTRAINT [FK_Order_Information_form_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Order_information] CHECK CONSTRAINT [FK_Order_Information_form_Order_form]
GO
ALTER TABLE [dbo].[Order_to_supplier]  WITH CHECK ADD  CONSTRAINT [FK_Order_form_to_supplier_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Order_to_supplier] CHECK CONSTRAINT [FK_Order_form_to_supplier_Supplier_information]
GO
ALTER TABLE [dbo].[Order_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [FK_Order_form_to_supplier_information_Order_form_to_supplier] FOREIGN KEY([Order_form_to_supplier_id_FK])
REFERENCES [dbo].[Order_to_supplier] ([Order_form_to_supplier_id_PK])
GO
ALTER TABLE [dbo].[Order_to_supplier_information] CHECK CONSTRAINT [FK_Order_form_to_supplier_information_Order_form_to_supplier]
GO
ALTER TABLE [dbo].[Pick_up_order]  WITH NOCHECK ADD  CONSTRAINT [FK_Pick_up_order_Customer_information1] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Customer_information] ([Customer_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order] CHECK CONSTRAINT [FK_Pick_up_order_Customer_information1]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [FK_Pick_up_order_information_Pick_up_order] FOREIGN KEY([Pick_up_order_informatin_id_PK])
REFERENCES [dbo].[Pick_up_order] ([Pick_up_order_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [FK_Pick_up_order_information_Pick_up_order]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [FK_Pick_up_order_information_Product_information] FOREIGN KEY([Product_id_FK])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [FK_Pick_up_order_information_Product_information]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_form_Supplier_information]
GO
ALTER TABLE [dbo].[Purchase_information]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_information_Product_information] FOREIGN KEY([Product_id_FK])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Purchase_information] CHECK CONSTRAINT [FK_Purchase_form_information_Product_information]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_information_Quotation] FOREIGN KEY([Quotation_id_FK])
REFERENCES [dbo].[Quotation] ([Quotation_id_PK])
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [FK_Quotation_information_Quotation]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [FK_RFQ_information_RFQ] FOREIGN KEY([RFQ_id_FK])
REFERENCES [dbo].[RFQ] ([RFQ_id_PK])
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [FK_RFQ_information_RFQ]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [FK_Sales_batch_Admin_information] FOREIGN KEY([Admin_id_FK])
REFERENCES [dbo].[Admin_information] ([Admin_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Admin_information]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH NOCHECK ADD  CONSTRAINT [FK_Sales_batch_Customer_information1] FOREIGN KEY([Sales_batch_id_PK])
REFERENCES [dbo].[Customer_information] ([Customer_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Customer_information1]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [FK_Sales_batch_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Order_form]
GO
ALTER TABLE [dbo].[Sales_order]  WITH CHECK ADD  CONSTRAINT [FK_Sales_order_Product_information] FOREIGN KEY([Product_id_FK])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Sales_order] CHECK CONSTRAINT [FK_Sales_order_Product_information]
GO
ALTER TABLE [dbo].[Sales_order]  WITH CHECK ADD  CONSTRAINT [FK_Sales_order_Sales_batch] FOREIGN KEY([Sales_batch_id_FK])
REFERENCES [dbo].[Sales_batch] ([Sales_batch_id_PK])
GO
ALTER TABLE [dbo].[Sales_order] CHECK CONSTRAINT [FK_Sales_order_Sales_batch]
GO
ALTER TABLE [dbo].[Backorder_information]  WITH CHECK ADD  CONSTRAINT [CK_Backorder_information_idr1] CHECK  (([Num]>(0)))
GO
ALTER TABLE [dbo].[Backorder_information] CHECK CONSTRAINT [CK_Backorder_information_idr1]
GO
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [CK_Inventory_information_idr1] CHECK  (([Num]>(0)))
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [CK_Inventory_information_idr1]
GO
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [CK_Inventory_information_idr2] CHECK  (([Price]>(0)))
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [CK_Inventory_information_idr2]
GO
ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form] CHECK  (([Order_form_status]='未审核' OR [Order_form_status]='审核通过' OR [Order_form_status]='审核不通过' OR [Order_form_status]='取消' OR [Order_form_status]='进入销售' OR [Order_form_status]='完成'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form]
GO
ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_idr2] CHECK  (([Customer_id_FK] like '[0-1][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form_idr2]
GO
ALTER TABLE [dbo].[Order_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr3] CHECK  (([Product_category]='办公用品类' OR [Product_category]='书籍类' OR [Product_category]='实验器材类' OR [Product_category]='家具类' OR [Product_category]='其他'))
GO
ALTER TABLE [dbo].[Order_information] CHECK CONSTRAINT [CK_Order_Information_form_idr3]
GO
ALTER TABLE [dbo].[Order_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr4] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_information] CHECK CONSTRAINT [CK_Order_Information_form_idr4]
GO
ALTER TABLE [dbo].[Order_to_supplier]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_idr1] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Order_to_supplier] CHECK CONSTRAINT [CK_Order_form_to_supplier_idr1]
GO
ALTER TABLE [dbo].[Order_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr1] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr1]
GO
ALTER TABLE [dbo].[Order_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr2] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr2]
GO
ALTER TABLE [dbo].[Order_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr3] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Order_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr3]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [CK_Pick_up_order_information_idr1] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [CK_Pick_up_order_information_idr1]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [CK_Pick_up_order_information_idr2] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [CK_Pick_up_order_information_idr2]
GO
ALTER TABLE [dbo].[Product_information]  WITH CHECK ADD  CONSTRAINT [CK_Product_information_idr2] CHECK  (([Product_category]='办公用品类' OR [Product_category]='书籍类' OR [Product_category]='实验器材类' OR [Product_category]='家具类' OR [Product_category]='其他'))
GO
ALTER TABLE [dbo].[Product_information] CHECK CONSTRAINT [CK_Product_information_idr2]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_idr1] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [CK_Purchase_form_idr1]
GO
ALTER TABLE [dbo].[Purchase_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr1] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Purchase_information] CHECK CONSTRAINT [CK_Purchase_form_information_idr1]
GO
ALTER TABLE [dbo].[Purchase_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr2] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Purchase_information] CHECK CONSTRAINT [CK_Purchase_form_information_idr2]
GO
ALTER TABLE [dbo].[Purchase_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr3] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Purchase_information] CHECK CONSTRAINT [CK_Purchase_form_information_idr3]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr2] CHECK  (([Quotation_source]='仓库' OR [Quotation_source]='供货商' OR [Quotation_source]='其他'))
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr2]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr6] CHECK  (([Price]>(0)))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr6]
GO
ALTER TABLE [dbo].[RFQ]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_idr2] CHECK  (([RFQ_status]='关闭' OR [RFQ_status]='等待'))
GO
ALTER TABLE [dbo].[RFQ] CHECK CONSTRAINT [CK_RFQ_idr2]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr3] CHECK  (([Product_category]='办公用品类' OR [Product_category]='书籍类' OR [Product_category]='实验器材类' OR [Product_category]='家具类' OR [Product_category]='其他'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr3]
GO
USE [master]
GO
ALTER DATABASE [LPS-Database] SET  READ_WRITE 
GO
