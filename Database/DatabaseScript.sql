USE [master]
GO
/****** Object:  Database [LPS-Database]    Script Date: 2019/11/28 22:21:33 ******/
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
/****** Object:  User [Colin]    Script Date: 2019/11/28 22:21:33 ******/
CREATE USER [Colin] FOR LOGIN [Colin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Admin_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin_information](
	[Admin_id_PK] [char](4) NOT NULL,
	[Admin_name] [char](15) NOT NULL,
	[Admin_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Admin_information] PRIMARY KEY CLUSTERED 
(
	[Admin_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class_information](
	[Class_id_PK] [char](6) NOT NULL,
	[College_name] [char](30) NOT NULL,
	[Class_contact] [char](15) NULL,
	[Class_contact_phone] [char](50) NULL,
	[Class_contact_email] [char](30) NULL,
	[Class_password] [char](30) NOT NULL,
 CONSTRAINT [PK_class_information] PRIMARY KEY CLUSTERED 
(
	[Class_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory_information](
	[Product_id] [char](13) NOT NULL,
	[Num] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Place] [char](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_form]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_form](
	[Order_form_id_PK] [char](6) NOT NULL,
	[Customer_id_FK] [char](6) NOT NULL,
	[Create_date] [datetime] NOT NULL,
	[Order_form_status] [char](6) NOT NULL,
	[Order_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Order_form] PRIMARY KEY CLUSTERED 
(
	[Order_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_form_to_supplier]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_form_to_supplier](
	[Order_form_to_supplier_id_PK] [char](6) NOT NULL,
	[Supplier_id_FK] [char](6) NOT NULL,
	[Price_of_all] [float] NOT NULL,
	[Order_form_to_supplier_createdate] [datetime] NOT NULL,
	[Order_form_to_supplier_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Order_form_to_supplier] PRIMARY KEY CLUSTERED 
(
	[Order_form_to_supplier_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_form_to_supplier_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_form_to_supplier_information](
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
/****** Object:  Table [dbo].[Order_Information_form]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Information_form](
	[Order_Information_form_id_PK] [char](6) NOT NULL,
	[Order_form_id_FK] [char](6) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
 CONSTRAINT [PK_Order_Information_form] PRIMARY KEY CLUSTERED 
(
	[Order_Information_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pick_up_order]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pick_up_order](
	[Pick_up_order_id_PK] [char](6) NOT NULL,
	[Customer_id_FK] [char](6) NOT NULL,
	[Pick_up_order_createdate] [datetime] NOT NULL,
 CONSTRAINT [PK_Pick_up_order] PRIMARY KEY CLUSTERED 
(
	[Pick_up_order_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pick_up_order_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pick_up_order_information](
	[Pick_up_order_informatin_id_PK] [char](6) NOT NULL,
	[Pick_up_order_id_FK] [char](6) NOT NULL,
	[Product_id_FK] [char](13) NOT NULL,
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
/****** Object:  Table [dbo].[Product_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_information](
	[Product_id_PK] [char](13) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_model] [char](30) NOT NULL,
 CONSTRAINT [PK_Product_information] PRIMARY KEY CLUSTERED 
(
	[Product_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase_form]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase_form](
	[Purchase_form_id_PK] [char](6) NOT NULL,
	[Supplier_id_FK] [char](6) NOT NULL,
	[Purchase_form_createdate] [datetime] NOT NULL,
	[Price_of_all] [float] NOT NULL,
	[Purchase_form_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Purchase_form] PRIMARY KEY CLUSTERED 
(
	[Purchase_form_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase_form_information]    Script Date: 2019/11/28 22:21:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase_form_information](
	[Purchase_form_information_id_PK] [char](6) NOT NULL,
	[Product_id_FK] [char](13) NOT NULL,
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
/****** Object:  Table [dbo].[Quotation]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation](
	[Quotation_id_PK] [char](6) NOT NULL,
	[Quotation_source] [char](8) NOT NULL,
	[Admin_id_FK] [char](4) NOT NULL,
	[Quotation_createdate] [datetime] NOT NULL,
	[Quotation_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED 
(
	[Quotation_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation_information]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation_information](
	[Quotation_information_id_PK] [char](6) NOT NULL,
	[Quotation_id_FK] [char](6) NOT NULL,
	[Admin_id_FK] [char](4) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Price_of_product] [float] NOT NULL,
 CONSTRAINT [PK_Quotation_information] PRIMARY KEY CLUSTERED 
(
	[Quotation_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RFQ]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RFQ](
	[RFQ_id_PK] [char](6) NOT NULL,
	[RFQ_createdate] [datetime] NOT NULL,
	[RFQ_status] [char](6) NOT NULL,
	[RFQ_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_RFQ] PRIMARY KEY CLUSTERED 
(
	[RFQ_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RFQ_information]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RFQ_information](
	[RFQ_information_id_PK] [char](6) NOT NULL,
	[RFQ_id_FK] [char](6) NOT NULL,
	[Product_category] [char](12) NOT NULL,
	[Product_name] [char](30) NOT NULL,
	[Product_modle] [char](30) NOT NULL,
	[Num_of_product] [int] NOT NULL,
	[Price_of_product] [float] NOT NULL,
 CONSTRAINT [PK_RFQ_information] PRIMARY KEY CLUSTERED 
(
	[RFQ_information_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales_batch]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_batch](
	[Sales_batch_id_PK] [char](6) NOT NULL,
	[Customer_id_FK] [char](6) NOT NULL,
	[Order_form_id_FK] [char](6) NOT NULL,
	[Source_of_goods] [char](6) NOT NULL,
	[Admin_id_FK] [char](4) NOT NULL,
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
/****** Object:  Table [dbo].[Sales_order]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_order](
	[Sales_order_id_PK] [char](6) NOT NULL,
	[Sales_batch_id_FK] [char](6) NOT NULL,
	[Product_id_FK] [char](13) NOT NULL,
	[Price_of_product] [float] NOT NULL,
	[Sales_order_notes] [char](70) NOT NULL,
 CONSTRAINT [PK_Sales_order] PRIMARY KEY CLUSTERED 
(
	[Sales_order_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[School_information]    Script Date: 2019/11/28 22:21:34 ******/
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
/****** Object:  Table [dbo].[Staff_information]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff_information](
	[Staff_id_PK] [char](6) NOT NULL,
	[Staff_name] [char](15) NOT NULL,
	[Staff_phone] [char](50) NOT NULL,
	[Staff_email] [char](30) NOT NULL,
	[Staff_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Staff_information] PRIMARY KEY CLUSTERED 
(
	[Staff_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier_information]    Script Date: 2019/11/28 22:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_information](
	[Supplier_id_PK] [char](6) NOT NULL,
	[Supplier_name] [char](20) NOT NULL,
	[Supplier_phone] [char](50) NOT NULL,
	[Supplier_email] [char](30) NOT NULL,
	[Supplier_password] [char](30) NOT NULL,
 CONSTRAINT [PK_Supplier_information] PRIMARY KEY CLUSTERED 
(
	[Supplier_id_PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Class_information] ([Class_id_PK], [College_name], [Class_contact], [Class_contact_phone], [Class_contact_email], [Class_password]) VALUES (N'000000', N'计算机学院                    ', NULL, NULL, NULL, N'000000                        ')
INSERT [dbo].[Class_information] ([Class_id_PK], [College_name], [Class_contact], [Class_contact_phone], [Class_contact_email], [Class_password]) VALUES (N'001601', N'计算机学院                    ', NULL, NULL, NULL, N'001601                        ')
INSERT [dbo].[Class_information] ([Class_id_PK], [College_name], [Class_contact], [Class_contact_phone], [Class_contact_email], [Class_password]) VALUES (N'001602', N'计算机学院                    ', NULL, NULL, NULL, N'001602                        ')
INSERT [dbo].[Class_information] ([Class_id_PK], [College_name], [Class_contact], [Class_contact_phone], [Class_contact_email], [Class_password]) VALUES (N'001603', N'计算机学院                    ', NULL, NULL, NULL, N'001603                        ')
INSERT [dbo].[Class_information] ([Class_id_PK], [College_name], [Class_contact], [Class_contact_phone], [Class_contact_email], [Class_password]) VALUES (N'001604', N'计算机学院                    ', NULL, NULL, NULL, N'001604                        ')
INSERT [dbo].[Class_information] ([Class_id_PK], [College_name], [Class_contact], [Class_contact_phone], [Class_contact_email], [Class_password]) VALUES (N'001605', N'计算机学院                    ', NULL, NULL, NULL, N'001605                        ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'00    ', N'计算机学院                         ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'01    ', N'数学科学学院                        ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'02    ', N'教育信息学院                        ')
INSERT [dbo].[School_information] ([School_id], [School_name]) VALUES (N'03    ', N'美术学院                          ')
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_information_Product_information] FOREIGN KEY([Product_id])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [FK_Inventory_information_Product_information]
GO
ALTER TABLE [dbo].[Order_form]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_form_class_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Class_information] ([Class_id_PK])
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [FK_Order_form_class_information]
GO
ALTER TABLE [dbo].[Order_form]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_form_Staff_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Staff_information] ([Staff_id_PK])
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [FK_Order_form_Staff_information]
GO
ALTER TABLE [dbo].[Order_form_to_supplier]  WITH CHECK ADD  CONSTRAINT [FK_Order_form_to_supplier_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Order_form_to_supplier] CHECK CONSTRAINT [FK_Order_form_to_supplier_Supplier_information]
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [FK_Order_form_to_supplier_information_Order_form_to_supplier] FOREIGN KEY([Order_form_to_supplier_id_FK])
REFERENCES [dbo].[Order_form_to_supplier] ([Order_form_to_supplier_id_PK])
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information] CHECK CONSTRAINT [FK_Order_form_to_supplier_information_Order_form_to_supplier]
GO
ALTER TABLE [dbo].[Order_Information_form]  WITH CHECK ADD  CONSTRAINT [FK_Order_Information_form_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Order_Information_form] CHECK CONSTRAINT [FK_Order_Information_form_Order_form]
GO
ALTER TABLE [dbo].[Pick_up_order]  WITH NOCHECK ADD  CONSTRAINT [FK_Pick_up_order_Class_information1] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Class_information] ([Class_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order] CHECK CONSTRAINT [FK_Pick_up_order_Class_information1]
GO
ALTER TABLE [dbo].[Pick_up_order]  WITH NOCHECK ADD  CONSTRAINT [FK_Pick_up_order_Staff_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Staff_information] ([Staff_id_PK])
GO
ALTER TABLE [dbo].[Pick_up_order] CHECK CONSTRAINT [FK_Pick_up_order_Staff_information]
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
ALTER TABLE [dbo].[Purchase_form]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_Supplier_information] FOREIGN KEY([Supplier_id_FK])
REFERENCES [dbo].[Supplier_information] ([Supplier_id_PK])
GO
ALTER TABLE [dbo].[Purchase_form] CHECK CONSTRAINT [FK_Purchase_form_Supplier_information]
GO
ALTER TABLE [dbo].[Purchase_form_information]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_form_information_Product_information] FOREIGN KEY([Product_id_FK])
REFERENCES [dbo].[Product_information] ([Product_id_PK])
GO
ALTER TABLE [dbo].[Purchase_form_information] CHECK CONSTRAINT [FK_Purchase_form_information_Product_information]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_Admin_information] FOREIGN KEY([Admin_id_FK])
REFERENCES [dbo].[Admin_information] ([Admin_id_PK])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Admin_information]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_information_Admin_information] FOREIGN KEY([Admin_id_FK])
REFERENCES [dbo].[Admin_information] ([Admin_id_PK])
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [FK_Quotation_information_Admin_information]
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
ALTER TABLE [dbo].[Sales_batch]  WITH NOCHECK ADD  CONSTRAINT [FK_Sales_batch_Class_information1] FOREIGN KEY([Sales_batch_id_PK])
REFERENCES [dbo].[Class_information] ([Class_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Class_information1]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [FK_Sales_batch_Order_form] FOREIGN KEY([Order_form_id_FK])
REFERENCES [dbo].[Order_form] ([Order_form_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Order_form]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH NOCHECK ADD  CONSTRAINT [FK_Sales_batch_Staff_information] FOREIGN KEY([Customer_id_FK])
REFERENCES [dbo].[Staff_information] ([Staff_id_PK])
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [FK_Sales_batch_Staff_information]
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
ALTER TABLE [dbo].[Admin_information]  WITH CHECK ADD  CONSTRAINT [CK_Admin_information_idr1] CHECK  (([Admin_id_PK] like '[0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Admin_information] CHECK CONSTRAINT [CK_Admin_information_idr1]
GO
ALTER TABLE [dbo].[Class_information]  WITH CHECK ADD  CONSTRAINT [CK_Class_information_idr1] CHECK  (([Class_id_PK] like '[0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Class_information] CHECK CONSTRAINT [CK_Class_information_idr1]
GO
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [CK_Inventory_information_idr1] CHECK  (([Num]>(0)))
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [CK_Inventory_information_idr1]
GO
ALTER TABLE [dbo].[Inventory_information]  WITH CHECK ADD  CONSTRAINT [CK_Inventory_information_idr2] CHECK  (([Price]>(0)))
GO
ALTER TABLE [dbo].[Inventory_information] CHECK CONSTRAINT [CK_Inventory_information_idr2]
GO
ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_idr1] CHECK  (([Order_form_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form_idr1]
GO
ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_idr2] CHECK  (([Customer_id_FK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form_idr2]
GO
ALTER TABLE [dbo].[Order_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_idr3] CHECK  (([Order_form_status] like '[未付款][取消][已付款][待取货][完成][异常]'))
GO
ALTER TABLE [dbo].[Order_form] CHECK CONSTRAINT [CK_Order_form_idr3]
GO
ALTER TABLE [dbo].[Order_form_to_supplier]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_idr1] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Order_form_to_supplier] CHECK CONSTRAINT [CK_Order_form_to_supplier_idr1]
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr1] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr1]
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr2] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr2]
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information]  WITH CHECK ADD  CONSTRAINT [CK_Order_form_to_supplier_information_idr3] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Order_form_to_supplier_information] CHECK CONSTRAINT [CK_Order_form_to_supplier_information_idr3]
GO
ALTER TABLE [dbo].[Order_Information_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr1] CHECK  (([Order_Information_form_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Order_Information_form] CHECK CONSTRAINT [CK_Order_Information_form_idr1]
GO
ALTER TABLE [dbo].[Order_Information_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr2] CHECK  (([Order_form_id_FK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Order_Information_form] CHECK CONSTRAINT [CK_Order_Information_form_idr2]
GO
ALTER TABLE [dbo].[Order_Information_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr3] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Order_Information_form] CHECK CONSTRAINT [CK_Order_Information_form_idr3]
GO
ALTER TABLE [dbo].[Order_Information_form]  WITH CHECK ADD  CONSTRAINT [CK_Order_Information_form_idr4] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Order_Information_form] CHECK CONSTRAINT [CK_Order_Information_form_idr4]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [CK_Pick_up_order_information_idr1] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [CK_Pick_up_order_information_idr1]
GO
ALTER TABLE [dbo].[Pick_up_order_information]  WITH CHECK ADD  CONSTRAINT [CK_Pick_up_order_information_idr2] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Pick_up_order_information] CHECK CONSTRAINT [CK_Pick_up_order_information_idr2]
GO
ALTER TABLE [dbo].[Product_information]  WITH CHECK ADD  CONSTRAINT [CK_Product_information_idr1] CHECK  (([Product_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9][0~9][0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Product_information] CHECK CONSTRAINT [CK_Product_information_idr1]
GO
ALTER TABLE [dbo].[Product_information]  WITH CHECK ADD  CONSTRAINT [CK_Product_information_idr2] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Product_information] CHECK CONSTRAINT [CK_Product_information_idr2]
GO
ALTER TABLE [dbo].[Purchase_form]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_idr1] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Purchase_form] CHECK CONSTRAINT [CK_Purchase_form_idr1]
GO
ALTER TABLE [dbo].[Purchase_form_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr1] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Purchase_form_information] CHECK CONSTRAINT [CK_Purchase_form_information_idr1]
GO
ALTER TABLE [dbo].[Purchase_form_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr2] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Purchase_form_information] CHECK CONSTRAINT [CK_Purchase_form_information_idr2]
GO
ALTER TABLE [dbo].[Purchase_form_information]  WITH CHECK ADD  CONSTRAINT [CK_Purchase_form_information_idr3] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Purchase_form_information] CHECK CONSTRAINT [CK_Purchase_form_information_idr3]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr1] CHECK  (([Quotation_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr1]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr2] CHECK  (([Quotation_source] like '[仓库][供货商][其他]'))
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr2]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_idr3] CHECK  (([Admin_id_FK] like '[0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [CK_Quotation_idr3]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr1] CHECK  (([Quotation_id_FK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr1]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr2] CHECK  (([Quotation_id_FK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr2]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr3] CHECK  (([Admin_id_FK] like '[0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr3]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr4] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr4]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr5] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr5]
GO
ALTER TABLE [dbo].[Quotation_information]  WITH CHECK ADD  CONSTRAINT [CK_Quotation_information_idr6] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Quotation_information] CHECK CONSTRAINT [CK_Quotation_information_idr6]
GO
ALTER TABLE [dbo].[RFQ]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_idr1] CHECK  (([RFQ_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[RFQ] CHECK CONSTRAINT [CK_RFQ_idr1]
GO
ALTER TABLE [dbo].[RFQ]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_idr2] CHECK  (([RFQ_status] like '[关闭][等待]'))
GO
ALTER TABLE [dbo].[RFQ] CHECK CONSTRAINT [CK_RFQ_idr2]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr1] CHECK  (([RFQ_information_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr1]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr2] CHECK  (([RFQ_id_FK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr2]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr3] CHECK  (([Product_category] like '[办公用品类][书籍类][实验器材类][家具类][其他]'))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr3]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr4] CHECK  (([Num_of_product]>(0)))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr4]
GO
ALTER TABLE [dbo].[RFQ_information]  WITH CHECK ADD  CONSTRAINT [CK_RFQ_information_idr5] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[RFQ_information] CHECK CONSTRAINT [CK_RFQ_information_idr5]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [CK_Sales_batch_1_idr3] CHECK  (([Price_of_all]>(0)))
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [CK_Sales_batch_1_idr3]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [CK_Sales_batch_idr1] CHECK  (([Source_of_goods] like '[仓库][供应商][其他]'))
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [CK_Sales_batch_idr1]
GO
ALTER TABLE [dbo].[Sales_batch]  WITH CHECK ADD  CONSTRAINT [CK_Sales_batch_idr2] CHECK  (([Sales_batch_status] like '[未付款][取消][已付款][待取货][完成][异常]'))
GO
ALTER TABLE [dbo].[Sales_batch] CHECK CONSTRAINT [CK_Sales_batch_idr2]
GO
ALTER TABLE [dbo].[Sales_order]  WITH CHECK ADD  CONSTRAINT [CK_Sales_order_idr1] CHECK  (([Price_of_product]>(0)))
GO
ALTER TABLE [dbo].[Sales_order] CHECK CONSTRAINT [CK_Sales_order_idr1]
GO
ALTER TABLE [dbo].[Staff_information]  WITH CHECK ADD  CONSTRAINT [CK_Staff_information_idr1] CHECK  (([Staff_id_PK] like '[0~9][0~9][0~9][0~9][0~9][0~9]'))
GO
ALTER TABLE [dbo].[Staff_information] CHECK CONSTRAINT [CK_Staff_information_idr1]
GO
USE [master]
GO
ALTER DATABASE [LPS-Database] SET  READ_WRITE 
GO
