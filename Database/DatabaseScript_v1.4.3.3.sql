USE [master]
GO
/****** Object:  Database [LPS-Database]    Script Date: 2019/12/6 10:33:43 ******/

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
/****** Object:  User [Colin]    Script Date: 2019/12/6 10:33:44 ******/
CREATE USER [Colin] FOR LOGIN [Colin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Admin_information]    Script Date: 2019/12/6 10:33:44 ******/
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
/****** Object:  Table [dbo].[Customer_information]    Script Date: 2019/12/6 10:33:44 ******/
/* date 2019-12-7 Zinghw*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_information](
	[Customer_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[College_name] [char](30) NOT NULL,
	[Customer_contact] [char](15) NOT NULL,
	[Customer_contact_phone] [char](11) NULL,
	[Customer_contact_email] [char](30) NULL,
	
	[Customer_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Customer_information] PRIMARY KEY CLUSTERED 
(
	[Customer_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* date 2019-12-7 Zinghw*/
/****** Object:  Table [dbo].[Inventory_information]    Script Date: 2019/12/6 10:33:44 ******/
/* date 2019-12-7 Zinghw*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory_information](

	[Inventory_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Product_id] [int] IDENTITY(1,1) NOT NULL,
	[Num] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Place] [char](50) NOT NULL,
	CONSTRAINT [PK_Inventory_information] PRIMARY KEY CLUSTERED 
(
	[Inventory_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


) ON [PRIMARY]
GO
/* date 2019-12-7 Zinghw*/
/****** Object:  Table [dbo].[Order_form]    Script Date: 2019/12/6 10:33:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_form](
	[Order_form_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Customer_id_FK] [int] IDENTITY(1,1) NOT NULL,
	[Create_date] [datetime] NOT NULL,
	[Order_form_status] [char](10) NOT NULL,
	[Order_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Order_form] PRIMARY KEY CLUSTERED 
(
	[Order_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Backorder_information*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Backorder_information](

	[Backorder_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Product_id] [int] NOT NULL,
	[Num] [int] NOT NULL,
	[isHandled][bit] NOT NULL,
	
	
	CONSTRAINT [PK_Backorder_information] PRIMARY KEY CLUSTERED 
(
	[Backorder_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


) ON [PRIMARY]
GO


/*Purchase_notice*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase_notice](

	[Purchase_notice_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Order_form_id] [int] NOT NULL,
	[Purchase_notice_status] [char](6) NOT NULL,
	
	
	CONSTRAINT [PK_Purchase_notice] PRIMARY KEY CLUSTERED 
(
	[Purchase_notice_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


) ON [PRIMARY]
GO


/*Supplier_information*/
/*
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
	
	CONSTRAINT [PK_Supplier_id_PK] PRIMARY KEY CLUSTERED 
(
	[Supplier_id_PK_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


) ON [PRIMARY]
GO

*/




/****** Object:  Table [dbo].[Order_information]    Script Date: 2019/12/6 10:33:44 ******/
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
	[Order_information_Status] [int] NOT NULL DEFAULT 0 ,
	[Num_of_product] [int] NOT NULL,

 CONSTRAINT [PK_Order_Information_form] PRIMARY KEY CLUSTERED 
(
	[Order_Information_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_to_supplier]    Script Date: 2019/12/6 10:33:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_to_supplier](
	[Order_form_to_supplier_id_PK] [int] IDENTITY(1,1)NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	
	[Price_of_all] [money] NOT NULL,
	[Order_form_to_supplier_createdate] [datetime] NOT NULL,
	[Order_form_to_supplier_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Order_form_to_supplier] PRIMARY KEY CLUSTERED 
(
	[Order_form_to_supplier_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_to_supplier_information]    Script Date: 2019/12/6 10:33:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_to_supplier_information](
	[Order_form_to_supplier_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Order_form_to_supplier_id_FK] [int] NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Price_of_product] [money] NOT NULL,
 CONSTRAINT [PK_Order_form_to_supplier_information] PRIMARY KEY CLUSTERED 
(
	[Order_form_to_supplier_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pick_up_order]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pick_up_order](
	[Pick_up_order_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Customer_id_FK] [int] NOT NULL,
	[Order_form_id_FK][int] NOT NULL,
	[Pick_up_order_createdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Pick_up_order] PRIMARY KEY CLUSTERED 
(
	[Pick_up_order_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pick_up_order_information]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pick_up_order_information](
	[Pick_up_order_informatin_id_PK][int] IDENTITY(1,1)NOT NULL,
	[Pick_up_order_id_FK] [int] NOT NULL,
	[Product_id_FK] [char](13) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
 CONSTRAINT [PK_Pick_up_order_information] PRIMARY KEY CLUSTERED 
(
	[Pick_up_order_informatin_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_information]    Script Date: 2019/12/6 10:33:45 ******/
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
/****** Object:  Table [dbo].[Purchase]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase_form](
	[Purchase_form_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	[Order_form_id_FK][int] NOT NULL,
	[Purchase_form_createdate] [datetime] NOT NULL,
	[Price_of_all] [money] NOT NULL,
	[Purchase_form_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Purchase_form] PRIMARY KEY CLUSTERED 
(
	[Purchase_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase_information]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase_information](
	[Purchase_form_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Product_id_FK] [int] NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Price_of_product] [money] NOT NULL,
 CONSTRAINT [PK_Purchase_form_information] PRIMARY KEY CLUSTERED 
(
	[Purchase_form_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation](
	[Quotation_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Quotation_source] [char](8) NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	[Order_form_id_FK][int] NOT NULL,
	[Quotation_createdate] [datetime] NOT NULL,
	[Quotation_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED 
(
	[Quotation_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation_information]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation_information](
	[Quotation_information_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Quotation_id_FK] [int] NOT NULL,
	[Order_information_form_id_FK] [int] NOT NULL,
	/*[Admin_id_FK] [int] NOT NULL,*/
	/*[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,*/
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_Quotation_information] PRIMARY KEY CLUSTERED 
(
	[Quotation_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RFQ]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RFQ](
	[RFQ_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Order_form_id_FK][int] NOT NULL,
	[RFQ_createdate] [datetime] NOT NULL,
	[RFQ_status] [char](6) NOT NULL,
	[RFQ_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_RFQ] PRIMARY KEY CLUSTERED 
(
	[RFQ_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RFQ_information]    Script Date: 2019/12/6 10:33:45 ******/
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
	/*[Price_of_product] [money] NOT NULL,*/
 CONSTRAINT [PK_RFQ_information] PRIMARY KEY CLUSTERED 
(
	[RFQ_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales_batch]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_batch](
	[Sales_batch_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Customer_id_FK] [int] NOT NULL,
	[Order_form_id_FK] [int] NOT NULL,
	[Source_of_goods] [char](6) NOT NULL,
	[Admin_id_FK] [int] NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	[Price_of_all] [money] NOT NULL,
	[createdate] [datetime] NOT NULL,
	[Sales_batch_status] [char](6) NOT NULL,
	[Sales_batch_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Sales_batch] PRIMARY KEY CLUSTERED 
(
	[Sales_batch_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales_order]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_order](
	[Sales_order_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Sales_batch_id_FK] [int] NOT NULL,
	[Product_id_FK] [int] NOT NULL,
	[Price_of_product] [money] NOT NULL,
	[Num] [int]NOT NULL,
	[Supplier_id_FK] [int] NOT NULL,
	/*[Sales_order_notes] [char](70) NOT NULL,*/
 CONSTRAINT [PK_Sales_order] PRIMARY KEY CLUSTERED 
(
	[Sales_order_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[School_information]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[School_information](
	[School_id] [int] IDENTITY(1,1) NOT NULL,
	[School_name] [nchar](30) NOT NULL,
 CONSTRAINT [PK_School_information] PRIMARY KEY CLUSTERED 
(
	[School_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Staff_information]    Script Date: 2019/12/6 10:33:45 ******/
/* date 2019-12-8 Zinghw*/
/*SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff_information](
	[Staff_id_PK] [char](7) NOT NULL,
	[Staff_name] [char](20) NOT NULL,
	[Staff_phone] [char](50) NULL,
	[Staff_email] [char](30) NULL,
	[Staff_password] [char](30) NOT NULL,
	[Staff_college] [char](30) NOT NULL,
 CONSTRAINT [PK_Staff_information] PRIMARY KEY CLUSTERED 
(
	[Staff_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
*/
/* date 2019-12-8 Zinghw*/
/****** Object:  Table [dbo].[Supplier_information]    Script Date: 2019/12/6 10:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_information](
	[Supplier_id_PK] [int] IDENTITY(1,1) NOT NULL,
	[Supplier_name] [char](20) NOT NULL,
	[Supplier_phone] [char](11) NULL,
	[Supplier_email] [char](30) NULL,
	[Supplier_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Supplier_information] PRIMARY KEY CLUSTERED 
(
	[Supplier_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/* date 2019-12-7 Zinghw*/
INSERT [dbo].[Admin_information] ([Admin_id_PK], [Admin_name], [Admin_password]) VALUES (N'0000', N'费尔奇         ', N'0000                          ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'000000 ', N'计算机学院                    ', NULL, NULL, NULL, N'000000                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001601 ', N'计算机学院                    ', NULL, NULL, NULL, N'001601                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001602 ', N'计算机学院                    ', NULL, NULL, NULL, N'001602                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001603 ', N'计算机学院                    ', NULL, NULL, NULL, N'001603                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001604 ', N'计算机学院                    ', NULL, NULL, NULL, N'001604                        ')
INSERT [dbo].[Customer_information] ([Customer_id_PK], [College_name], [Customer_contact], [Customer_contact_phone], [Customer_contact_email], [Customer_password]) VALUES (N'001605 ', N'计算机学院                    ', NULL, NULL, NULL, N'001605                        ')
SET IDENTITY_INSERT [dbo].[Order_form] ON 
/* date 2019-12-7 Zinghw*/

INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (8, N'0123123', CAST(N'2019-12-05T11:06:01.513' AS DateTime), N'审核不通过', N'test                                                                  ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (9, N'0123123', CAST(N'2019-12-05T11:08:35.157' AS DateTime), N'未审核    ', N'test2                                                                 ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (10, N'0123123', CAST(N'2019-12-05T11:10:27.560' AS DateTime), N'审核通过  ', N'显卡要有十块的                                                        ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (11, N'0123123', CAST(N'2019-12-05T11:15:25.780' AS DateTime), N'未审核    ', N'笔记本要硬皮精装的                                                    ')
INSERT [dbo].[Order_form] ([Order_form_id_PK], [Customer_id_FK], [Create_date], [Order_form_status], [Order_notes]) VALUES (12, N'0123123', CAST(N'2019-12-06T09:10:58.603' AS DateTime), N'未审核    ', N'台灯要高级一点的。                                                    ')
SET IDENTITY_INSERT [dbo].[Order_form] OFF
SET IDENTITY_INSERT [dbo].[Order_information] ON 

INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (1, 10, N'实验器材类  ', N'深度学习服务器                ', N'GTX1050                       ', 1)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (2, 11, N'办公用品类  ', N'签字笔                        ', N'黑1.0                         ', 50)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (3, 11, N'办公用品类  ', N'笔记本                        ', N'A5                            ', 30)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (4, 11, N'办公用品类  ', N'印泥                          ', N'红                            ', 5)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (5, 12, N'家具类      ', N'电脑椅                        ', N'1.2m                          ', 10)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (6, 12, N'家具类      ', N'台灯                          ', N'20w                           ', 10)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (7, 12, N'家具类      ', N'沙发                          ', N'皮质                          ', 2)
INSERT [dbo].[Order_information] ([Order_Information_form_id_PK], [Order_form_id_FK], [Product_category], [Product_name], [Product_modle], [Num_of_product]) VALUES (8, 12, N'家具类      ', N'书架                          ', N'1.5m*1.8m                     ', 2)
SET IDENTITY_INSERT [dbo].[Order_information] OFF
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'00    ', N'计算机学院                         ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'01    ', N'数学科学学院                        ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'02    ', N'教育信息技术学院                        ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'03    ', N'美术学院                          ')
/* date 2019-12-8 Zinghw*/
INSERT [dbo].[Customer_information] ([Customer_id_PK], [Customer_contact_phone], [Customer_contact_email], [Customer_password], [College_name]) VALUES (N'0123123',  NULL, NULL, N'0123123                       ', N'计算机学院                    ')
/* date 2019-12-8 Zinghw*/
INSERT [dbo].[Supplier_information] ([Supplier_id_PK], [Supplier_name], [Supplier_phone], [Supplier_email], [Supplier_password]) VALUES (N'000001', N'京东小哥1           ', NULL, NULL, N'000001                        ')
ALTER TABLE [dbo].[Order_form] ADD  CONSTRAINT [DF_Order_Order_form_status]  DEFAULT ('未审核') FOR [Order_form_status]
GO


ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_information_Product_information] FOREIGN KEY([Product_id])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [FK_Inventory_information_Product_information]
GO
/*Backorder*/
ALTER TABLE [dbo].[Backorder_information]  WITH CHECK ADD  CONSTRAINT [FK_Backorder_information_Product_information] FOREIGN KEY([Product_id])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Backorder_information] CHECK CONSTRAINT [FK_Backorder_information_Product_information]
GO


/*Order_form_id,Purchase_notice*/
ALTER TABLE [dbo].[Purchase_notice]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_notice_Order_form_id] FOREIGN KEY([Order_form_id])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Purchase_notice] CHECK CONSTRAINT [FK_Purchase_notice_Order_form_id]
GO


/*date 2019-12-8 ZinghW*/

/*
ALTER TABLE [dbo].[Order_form]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_form_Staff_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Staff_information] ([Staff_id_PK])
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [FK_Order_form_Staff_information]
GO
*/
/*date 2019-12-8 ZinghW*/

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
/* date 2019-12-7 Zinghw*/
ALTER TABLE [dbo].[Pick_up_order]  WITH NOCHECK ADD  CONSTRAINT [FK_Pick_up_order_Customer_information1] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Customer_information] ([Customer_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order] CHECK CONSTRAINT [FK_Pick_up_order_Customer_information1]
GO




/*Sales_order,Supplier_id_FK*/
ALTER TABLE [dbo].[Sales_order]  WITH CHECK ADD  CONSTRAINT [FK_Sales_order_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Sales_order] CHECK CONSTRAINT [FK_Sales_order_Supplier_information]
GO

/*Sales_batch,Supplier_id_FK*/
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [FK_Sales_batch_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Supplier_information]
GO

/* date 2019-12-7 Zinghw*/

/* date 2019-12-8 Zinghw*/

/*ALTER TABLE [dbo].[Pick_up_order]  WITH NOCHECK ADD  CONSTRAINT [FK_Pick_up_order_Staff_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Staff_information] ([Staff_id_PK])
GO

ALTER TABLE [dbo].[Pick_up_order] CHECK CONSTRAINT [FK_Pick_up_order_Staff_information]
GO
*/
/* date 2019-12-8 Zinghw*/
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
ALTER TABLE [dbo].[Purchase_form]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Purchase_form] CHECK CONSTRAINT [FK_Purchase_form_Supplier_information]
GO
ALTER TABLE [dbo].[Purchase_information]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_information_Product_information] FOREIGN KEY([Product_id_FK])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Purchase_information] CHECK CONSTRAINT [FK_Purchase_form_information_Product_information]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Supplier_information]
GO
/*
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_information_Admin_information] FOREIGN KEY([Admin_id_FK])
REFERENCES [dbo].[Admin_information] ([Admin_id_PK])
GO

ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [FK_Quotation_information_Admin_information]
GO

*/
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_information_Quotation] FOREIGN KEY([Quotation_id_FK])
REFERENCES [dbo].[Quotation] ([Quotation_id_PK])
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [FK_Quotation_information_Quotation]
GO
/*[Quotation_information],RFQ_information_id_FK*/
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_information_Order_information_form_id] FOREIGN KEY([Order_information_form_id_FK])
REFERENCES [dbo].[Order_information_form] ([Order_information_form_id_PK])
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [FK_Quotation_information_Order_information_form_id]
GO



/*[Quotation,[Order_form_id_FK]*/
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Order_form]
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
/* date 2019-12-7 Zinghw*/
ALTER TABLE [dbo].[Sales_batch]  WITH NOCHECK ADD  CONSTRAINT [FK_Sales_batch_Customer_information1] FOREIGN KEY([Sales_batch_id_PK])
REFERENCES [dbo].[Customer_information] ([Customer_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Customer_information1]
GO
/* date 2019-12-7 Zinghw*/
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [FK_Sales_batch_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Order_form]
GO
/* data 2019-12-7 Zinghw*/
/*
ALTER TABLE [dbo].[Sales_batch]  WITH NOCHECK ADD  CONSTRAINT [FK_Sales_batch_Staff_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Staff_information] ([Staff_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Staff_information]
GO
*/
/* data 2019-12-7 Zinghw*/

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





/*询价单RFQ，，Purchase_form进货单、Pick_up_order  取货单都加上Order_form_id_FK*/

ALTER TABLE [dbo].[RFQ]  WITH CHECK ADD  CONSTRAINT [FK_RFQ_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[RFQ] CHECK CONSTRAINT [FK_RFQ_Order_form]
GO

ALTER TABLE [dbo].[Purchase_form]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Purchase_form] CHECK CONSTRAINT [FK_Purchase_form_Order_form]
GO

ALTER TABLE [dbo].[Pick_up_order]  WITH CHECK ADD  CONSTRAINT [FK_Pick_up_order_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order] CHECK CONSTRAINT [FK_Pick_up_order_Order_form]
GO



/*UNIQUE
ALTER TABLE 
     ADD CONSTRAINT _isbn UNIQUE(_id_PK)
GO
*/

ALTER TABLE Admin_information
     ADD CONSTRAINT Admin_information_isbn UNIQUE(Admin_id_PK)
GO

ALTER TABLE Customer_information
     ADD CONSTRAINT Customer_information_isbn UNIQUE(Customer_id_PK)
GO

ALTER TABLE Inventory_information
     ADD CONSTRAINT Inventory_information_isbn UNIQUE(Inventory_information_id_PK)
GO

ALTER TABLE Order_form
     ADD CONSTRAINT Order_form_isbn UNIQUE(Order_form_id_PK)
GO

ALTER TABLE Backorder_information
     ADD CONSTRAINT Backorder_information_isbn UNIQUE(Backorder_information_id_PK)
GO

ALTER TABLE Purchase_notice
     ADD CONSTRAINT Purchase_notice_isbn UNIQUE(Purchase_notice_id_PK)
GO

ALTER TABLE Order_information
     ADD CONSTRAINT Order_information_isbn UNIQUE(Order_Information_form_id_PK)
GO

ALTER TABLE Order_to_supplier
     ADD CONSTRAINT Order_to_supplier_isbn UNIQUE(Order_form_to_supplier_id_PK)
GO

ALTER TABLE Order_to_supplier_information
     ADD CONSTRAINT Order_to_supplier_information_isbn UNIQUE(Order_form_to_supplier_information_id_PK)
GO

ALTER TABLE Pick_up_order
     ADD CONSTRAINT Pick_up_order_isbn UNIQUE(Pick_up_order_id_PK)
GO

ALTER TABLE Pick_up_order_information
     ADD CONSTRAINT Pick_up_order_information_isbn UNIQUE(Pick_up_order_information_id_PK)
GO

ALTER TABLE Product_information
     ADD CONSTRAINT Product_information_isbn UNIQUE(Product_id_PK)
GO

ALTER TABLE  [Purchase_form]
     ADD CONSTRAINT Purchase_isbn UNIQUE(Purchase_form_id_PK)

GO

ALTER TABLE Purchase_information
     ADD CONSTRAINT Purchase_information_isbn UNIQUE(Purchase_form_information_id_PK)
GO

ALTER TABLE Quotation
     ADD CONSTRAINT Quotation_isbn UNIQUE(Quotation_id_PK)
GO

ALTER TABLE Quotation_information
     ADD CONSTRAINT Quotation_information_isbn UNIQUE(Quotation_information_id_PK)
GO

ALTER TABLE RFQ
     ADD CONSTRAINT RFQ_isbn UNIQUE(RFQ_id_PK)
GO

ALTER TABLE RFQ_information
     ADD CONSTRAINT RFQ_information_isbn UNIQUE(RFQ_information_id_PK)
GO

ALTER TABLE Sales_batch
     ADD CONSTRAINT Sales_batch_isbn UNIQUE(Sales_batch_id_PK)
GO

ALTER TABLE Sales_order
     ADD CONSTRAINT Sales_order_isbn UNIQUE(Sales_order_id_PK)
GO

ALTER TABLE School_information
     ADD CONSTRAINT School_information_isbn UNIQUE(School_id)
GO

ALTER TABLE Supplier_information
     ADD CONSTRAINT Supplier_information_isbn UNIQUE(Supplier_id_PK)
GO




/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[Admin_information]  WITH CHECK ADD  CONSTRAINT [CK_Admin_information_idr1] CHECK  (([Admin_id_PK] like '[0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Admin_information] CHECK CONSTRAINT [CK_Admin_information_idr1]
GO
/* data 2019-12-7 Zinghw*/
ALTER TABLE [dbo].[Customer_information]  WITH CHECK ADD  CONSTRAINT [CK_Customer_information_idr1] CHECK  (([Customer_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Customer_information] CHECK CONSTRAINT [CK_Customer_information_idr1]
GO
*/
/* date 2019-12-10 Zinghw*/
/* data 2019-12-7 Zinghw*/
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [CK_Inventory_information_idr1] CHECK  (([Num]>(0)))
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [CK_Inventory_information_idr1]
GO
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [CK_Inventory_information_idr2] CHECK  (([Price]>(0)))
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [CK_Inventory_information_idr2]
GO
/*Backorder*/
ALTER TABLE [dbo].[Backorder_information]  WITH CHECK ADD  CONSTRAINT [CK_Backorder_information_idr1] CHECK  (([Num]>(0)))
GO
ALTER TABLE [dbo].[Backorder_information] CHECK CONSTRAINT [CK_Backorder_information_idr1]
GO


/*Purchase_notice,Purchase_notice_status*/
ALTER TABLE [dbo].[Purchase_notice]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_notice_idr1] CHECK  (([Purchase_notice_status]='已到货' OR [Purchase_notice_status]='进货中'OR [Purchase_notice_status]='待进货'))
GO
ALTER TABLE [dbo].[Purchase_notice] CHECK CONSTRAINT [CK_Purchase_notice_idr1]
GO




ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form] CHECK  (([Order_form_status]='未审核' OR [Order_form_status]='审核通过' OR [Order_form_status]='审核不通过' OR [Order_form_status]='取消' OR [Order_form_status]='进入销售' OR [Order_form_status]='完成'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form]
GO

/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_idr2] CHECK  (([Customer_id_FK] like '[0-1][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form_idr2]
GO
 
*/
/* date 2019-12-10 Zinghw*/
ALTER TABLE [dbo].[Order_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr3] CHECK  (([Product_category]='办公用品类' OR [Product_category]='书籍类' OR [Product_category]='实验器材类' OR [Product_category]='家具类' OR [Product_category]='其他'))
GO
ALTER TABLE [dbo].[Order_information] CHECK CONSTRAINT [CK_Order_Information_form_idr3]
GO
ALTER TABLE [dbo].[Order_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr4] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_information] CHECK CONSTRAINT [CK_Order_Information_form_idr4]
GO
ALTER TABLE [dbo].[Order_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr5] CHECK  (([Order_information_Status]=(0) OR [Order_information_Status]=(1) OR [Order_information_Status]=(2)OR [Order_information_Status]=(3)))

GO
ALTER TABLE [dbo].[Order_information] CHECK CONSTRAINT [CK_Order_Information_form_idr5]
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
ALTER TABLE [dbo].[Order_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr3] CHECK  (([Product_category]= '办公用品类' OR [Product_category]= '书籍类' OR [Product_category]= '实验器材类' OR [Product_category]= '家具类' OR [Product_category]= '其他]'))
GO
ALTER TABLE [dbo].[Order_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr3]
GO

/* data 2019-12-7 Zinghw*/

/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[Supplier_information]  WITH CHECK ADD  CONSTRAINT [PK_Supplier_information_idr1] CHECK  (([Supplier_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Supplier_information] CHECK CONSTRAINT [PK_Supplier_information_idr1]
GO
*/
/* date 2019-12-10 Zinghw*/
/* data 2019-12-7 Zinghw*/

ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [CK_Pick_up_order_information_idr1] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [CK_Pick_up_order_information_idr1]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [CK_Pick_up_order_information_idr2] CHECK  (([Product_category] = '办公用品类' OR [Product_category]= '书籍类' OR [Product_category]= '实验器材类' OR [Product_category]= '家具类' OR [Product_category]= '其他'))
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [CK_Pick_up_order_information_idr2]
GO
/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[Product_information]  WITH CHECK ADD  CONSTRAINT [CK_Product_information_idr1] CHECK  (([Product_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO

ALTER TABLE [dbo].[Product_information] CHECK CONSTRAINT [CK_Product_information_idr1]
GO
*/
/* date 2019-12-10 Zinghw*/
ALTER TABLE [dbo].[Product_information]  WITH CHECK ADD  CONSTRAINT [CK_Product_information_idr2] CHECK  (([Product_category] = '办公用品类' OR [Product_category]= '书籍类' OR [Product_category]= '实验器材类' OR [Product_category]= '家具类' OR [Product_category]= '其他'))
GO
ALTER TABLE [dbo].[Product_information] CHECK CONSTRAINT [CK_Product_information_idr2]
GO
ALTER TABLE [dbo].[Purchase_form]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_idr1] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Purchase_form] CHECK CONSTRAINT [CK_Purchase_form_idr1]
GO
ALTER TABLE [dbo].[Purchase_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr1] CHECK  (([Product_category] = '办公用品类' OR [Product_category]= '书籍类' OR [Product_category]= '实验器材类' OR [Product_category]= '家具类' OR [Product_category]= '其他'))
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
/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr1] CHECK  (([Quotation_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO

ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr1]
GO
*/
/* date 2019-12-10 Zinghw*/
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr2] CHECK  (([Quotation_source] = '仓库' OR [Quotation_source]= '供货商' OR [Quotation_source]= '其他'))
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr2]
GO

/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr3] CHECK  (([Admin_id_FK] like '[0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr3]
GO


ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr1] CHECK  (([Quotation_id_FK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr1]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr2] CHECK  (([Quotation_id_FK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr2]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr3] CHECK  (([Admin_id_FK] like '[0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr3]
GO
*/
/* date 2019-12-10 Zinghw*/

/*ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr4] CHECK  (([Product_category] like '办公用品类' OR [Product_category]= '书籍类' OR [Product_category]= '实验器材类' OR [Product_category]= '家具类' OR [Product_category]= '其他'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr4]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr5] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr5]
GO
*/
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr6] CHECK  (([Price]>(0)))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr6]
GO

/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[RFQ]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_idr1] CHECK  (([RFQ_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[RFQ] CHECK CONSTRAINT [CK_RFQ_idr1]
GO
*/
/* date 2019-12-10 Zinghw*/
ALTER TABLE [dbo].[RFQ]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_idr2] CHECK  (([RFQ_status] = '询价中' OR [RFQ_status]='询价完成' OR [RFQ_status]='询价取消'))


GO
ALTER TABLE [dbo].[RFQ] CHECK CONSTRAINT [CK_RFQ_idr2]
GO

/* date 2019-12-10 Zinghw*/
/*
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr1] CHECK  (([RFQ_information_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr1]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr2] CHECK  (([RFQ_id_FK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr2]
GO
*/
/* date 2019-12-10 Zinghw*/
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr3] CHECK  (([Product_category] = '办公用品类' OR [Product_category]= '书籍类' OR [Product_category]= '实验器材类' OR [Product_category]= '家具类' OR [Product_category]= '其他'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr3]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr4] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr4]
GO
/*
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr5] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr5]
GO

*/
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [CK_Sales_batch_1_idr3] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [CK_Sales_batch_1_idr3]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [CK_Sales_batch_idr1] CHECK  (([Source_of_goods] = '仓库' OR [Source_of_goods]= '供应商' OR [Source_of_goods]= '其他'))
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [CK_Sales_batch_idr1]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [CK_Sales_batch_idr2] CHECK  (([Sales_batch_status] = '未付款' OR [Sales_batch_status]= '取消' OR [Sales_batch_status]= '已付款' OR [Sales_batch_status]= '待取货' OR [Sales_batch_status]= '完成' OR [Sales_batch_status]= '异常'))
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [CK_Sales_batch_idr2]
GO
ALTER TABLE [dbo].[Sales_order]  WITH CHECK ADD  CONSTRAINT [CK_Sales_order_idr1] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Sales_order] CHECK CONSTRAINT [CK_Sales_order_idr1]
GO
/* data 2019-12-7 Zinghw*/
/*
ALTER TABLE [dbo].[Staff_information]  WITH CHECK ADD  CONSTRAINT [CK_Staff_information_idr1] CHECK  (([Staff_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Staff_information] CHECK CONSTRAINT [CK_Staff_information_idr1]
GO
*/
ALTER TABLE [dbo].[Customer_information]  WITH CHECK ADD  CONSTRAINT [CK_Customer_information_idr2] CHECK  (([College_name] = '教育科学学院' OR [College_name]= '特殊教育学院' OR [College_name]= '哲学与社会发展学院' OR [College_name]= '马克思主义学院' OR [College_name]= '历史文化学院' OR [College_name]= '外国语言文化学院' OR [College_name]= '国际文化学院' OR [College_name]= '美术学院' OR [College_name]= '教育信息技术学院' OR [College_name]= '数学科学学院' OR [College_name]= '生命科学学院' OR [College_name]= '地理科学学院' OR 
[College_name]= '计算机学院' OR [College_name]= '心理学院' OR [College_name]= '继续教育学院' OR [College_name]= '网络教育学院' OR [College_name]= '光电子材料与技术研究所' OR [College_name]= '生物光子学研究院' OR [College_name]= '脑科学与康复医学研究院' OR [College_name]= '华南数学应用与交叉研究中心' OR [College_name]= '人文社会科学高等研究院' OR [College_name]= '教师教育学部' OR [College_name]= '广东省基础教育与信息化研究院' OR [College_name]= '心理学部' OR [College_name]= '（清远）科技创新研究院' OR [College_name]= '文学院' OR 
[College_name]= '经济与管理学院' OR [College_name]= '法学院' OR [College_name]= '政治与公共管理学院' OR [College_name]= '体育科学学院' OR [College_name]= '音乐学院' OR [College_name]= '物理与电信工程学院' OR [College_name]= '化学与环境学院' OR [College_name]= '旅游管理学院' OR [College_name]= '信息光电子科技学院' OR [College_name]= '华南先进光电子研究院' OR [College_name]= '量子物质研究院' OR [College_name]= '物理前沿科学研究院' OR [College_name]= '环境研究院' OR [College_name]= '物理学部' OR [College_name]= '创业学院' OR 
[College_name]= '科学技术与社会研究院' OR [College_name]= '城市文化学院' OR [College_name]= '国际商学院' OR [College_name]= '软件学院' OR [College_name]= '职业教育学院' OR [College_name]= '国际联合学院' OR [College_name]= '南海学院'))
GO
ALTER TABLE [dbo].[Customer_information] CHECK CONSTRAINT [CK_Customer_information_idr2]
GO

/* data 2019-12-7 Zinghw*/


USE [master]
GO
ALTER DATABASE [LPS-Database] SET  READ_WRITE 
GO
