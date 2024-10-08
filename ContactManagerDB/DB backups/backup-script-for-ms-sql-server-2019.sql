USE [master]
GO
/****** Object:  Database [ContactsManagment]    Script Date: 9/16/2024 1:57:36 PM ******/
CREATE DATABASE [ContactsManagment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ContactsManagment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ContactsManagment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ContactsManagment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ContactsManagment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContactsManagment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ContactsManagment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ContactsManagment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ContactsManagment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ContactsManagment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ContactsManagment] SET ARITHABORT OFF 
GO
ALTER DATABASE [ContactsManagment] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ContactsManagment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ContactsManagment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ContactsManagment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ContactsManagment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ContactsManagment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ContactsManagment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ContactsManagment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ContactsManagment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ContactsManagment] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ContactsManagment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ContactsManagment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ContactsManagment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ContactsManagment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ContactsManagment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ContactsManagment] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ContactsManagment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ContactsManagment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ContactsManagment] SET  MULTI_USER 
GO
ALTER DATABASE [ContactsManagment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ContactsManagment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ContactsManagment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ContactsManagment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ContactsManagment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ContactsManagment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ContactsManagment] SET QUERY_STORE = ON
GO
ALTER DATABASE [ContactsManagment] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ContactsManagment]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/16/2024 1:57:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 9/16/2024 1:57:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Married] [bit] NOT NULL,
	[Phone] [nvarchar](450) NOT NULL,
	[Salary] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240912121703_InitialContactsDB', N'6.0.33')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240916085109_AddPhoneIndexAndPrecision', N'6.0.33')
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (1, N'Gussie Allison', CAST(N'1988-08-06T00:00:00.0000000' AS DateTime2), 1, N'(286) 975-9844', CAST(600.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (2, N'Katie Kim', CAST(N'1989-11-21T00:00:00.0000000' AS DateTime2), 0, N'(683) 931-1009', CAST(400.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (3, N'Josie Gill', CAST(N'1993-12-20T00:00:00.0000000' AS DateTime2), 0, N'(820) 847-9489', CAST(550.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (4, N'Kenneth Welch', CAST(N'1982-07-13T00:00:00.0000000' AS DateTime2), 0, N'(815) 733-8244', CAST(763.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (5, N'Lela Knight', CAST(N'1980-04-18T00:00:00.0000000' AS DateTime2), 1, N'(658) 289-1461', CAST(897.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (6, N'Lottie Grant', CAST(N'1995-03-06T00:00:00.0000000' AS DateTime2), 0, N'(625) 933-3588', CAST(764.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (7, N'Jordan Park', CAST(N'2000-05-09T00:00:00.0000000' AS DateTime2), 1, N'(232) 704-8902', CAST(900.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (8, N'Eliza Maxwell', CAST(N'1984-02-04T00:00:00.0000000' AS DateTime2), 0, N'(923) 720-7277', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (9, N'Harriet Peters', CAST(N'1981-01-10T00:00:00.0000000' AS DateTime2), 1, N'(630) 336-9786', CAST(800.00 AS Decimal(18, 2)))
INSERT [dbo].[Contacts] ([Id], [Name], [DateOfBirth], [Married], [Phone], [Salary]) VALUES (10, N'Johanna Underwood', CAST(N'1997-10-17T00:00:00.0000000' AS DateTime2), 0, N'(687) 207-7117', CAST(700.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Contacts_Phone]    Script Date: 9/16/2024 1:57:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Contacts_Phone] ON [dbo].[Contacts]
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ContactsManagment] SET  READ_WRITE 
GO
