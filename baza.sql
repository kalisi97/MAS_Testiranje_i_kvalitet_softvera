USE [master]
GO
/****** Object:  Database [Baza]    Script Date: 4/10/2021 1:21:13 PM ******/
CREATE DATABASE [Baza]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Baza', FILENAME = N'C:\Users\user\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Baza.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Baza_log', FILENAME = N'C:\Users\user\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Baza.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Baza] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Baza].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Baza] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [Baza] SET ANSI_NULLS ON 
GO
ALTER DATABASE [Baza] SET ANSI_PADDING ON 
GO
ALTER DATABASE [Baza] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [Baza] SET ARITHABORT ON 
GO
ALTER DATABASE [Baza] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Baza] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Baza] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Baza] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Baza] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [Baza] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [Baza] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Baza] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [Baza] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Baza] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Baza] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Baza] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Baza] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Baza] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Baza] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Baza] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Baza] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Baza] SET RECOVERY FULL 
GO
ALTER DATABASE [Baza] SET  MULTI_USER 
GO
ALTER DATABASE [Baza] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Baza] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Baza] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Baza] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Baza] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Baza] SET QUERY_STORE = OFF
GO
USE [Baza]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Baza]
GO
/****** Object:  Table [dbo].[Mesto]    Script Date: 4/10/2021 1:21:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesto](
	[MestoId] [int] NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Ptt] [int] NOT NULL,
	[BrojStanovnika] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MestoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Osoba]    Script Date: 4/10/2021 1:21:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Osoba](
	[OsobaId] [int] NOT NULL,
	[MaticniBroj] [varchar](13) NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[Visina] [int] NULL,
	[Tezina] [int] NULL,
	[BojaOciju] [int] NULL,
	[Telefon] [varchar](20) NULL,
	[Email] [varchar](50) NOT NULL,
	[DatumRodjenja] [date] NULL,
	[Adresa] [nvarchar](50) NULL,
	[Napomena] [nvarchar](50) NULL,
	[MestoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OsobaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Mesto] ([MestoId], [Naziv], [Ptt], [BrojStanovnika]) VALUES (1, N'Prokuplje', 18400, 80000)
INSERT [dbo].[Mesto] ([MestoId], [Naziv], [Ptt], [BrojStanovnika]) VALUES (2, N'Beograd', 11000, NULL)
GO
INSERT [dbo].[Osoba] ([OsobaId], [MaticniBroj], [Ime], [Prezime], [Visina], [Tezina], [BojaOciju], [Telefon], [Email], [DatumRodjenja], [Adresa], [Napomena], [MestoId]) VALUES (1, N'1234567892224', N'Katarina', N'Simic', 163, 47, 1, N'+381642119548', N'katarinasimic1997@outlook.rs', CAST(N'1997-04-17' AS Date), N'XXI srpske divizje br. 64', N'nema', 1)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Osoba__CDE6A7A2AA950B49]    Script Date: 4/10/2021 1:21:14 PM ******/
ALTER TABLE [dbo].[Osoba] ADD UNIQUE NONCLUSTERED 
(
	[MaticniBroj] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Osoba]  WITH CHECK ADD  CONSTRAINT [FK_Osoba_Mesto] FOREIGN KEY([MestoId])
REFERENCES [dbo].[Mesto] ([MestoId])
GO
ALTER TABLE [dbo].[Osoba] CHECK CONSTRAINT [FK_Osoba_Mesto]
GO
ALTER TABLE [dbo].[Mesto]  WITH CHECK ADD  CONSTRAINT [CK_Mesto_BrojStanovnika] CHECK  (([BrojStanovnika]>(0)))
GO
ALTER TABLE [dbo].[Mesto] CHECK CONSTRAINT [CK_Mesto_BrojStanovnika]
GO
ALTER TABLE [dbo].[Mesto]  WITH CHECK ADD  CONSTRAINT [CK_Mesto_Ptt] CHECK  (([Ptt]>=(11000)))
GO
ALTER TABLE [dbo].[Mesto] CHECK CONSTRAINT [CK_Mesto_Ptt]
GO
ALTER TABLE [dbo].[Osoba]  WITH CHECK ADD  CONSTRAINT [CK_Osoba_Email] CHECK  (([Email] like '%.rs'))
GO
ALTER TABLE [dbo].[Osoba] CHECK CONSTRAINT [CK_Osoba_Email]
GO
ALTER TABLE [dbo].[Osoba]  WITH CHECK ADD  CONSTRAINT [CK_Osoba_Tezina] CHECK  (([Tezina]<(250)))
GO
ALTER TABLE [dbo].[Osoba] CHECK CONSTRAINT [CK_Osoba_Tezina]
GO
ALTER TABLE [dbo].[Osoba]  WITH CHECK ADD  CONSTRAINT [CK_Osoba_Visina] CHECK  (([Visina]>(35)))
GO
ALTER TABLE [dbo].[Osoba] CHECK CONSTRAINT [CK_Osoba_Visina]
GO
/****** Object:  StoredProcedure [dbo].[PROC_INSERT_MESTO]    Script Date: 4/10/2021 1:21:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PROC_INSERT_MESTO]

 @MestoId as INTEGER,
 @Naziv NVARCHAR(50),
 @Ptt INTEGER, 
 @BrojStanovnika INT = null

AS  
BEGIN  

INSERT INTO Mesto
VALUES (@MestoId, @Naziv, @Ptt, @BrojStanovnika)  

END
GO
/****** Object:  StoredProcedure [dbo].[PROC_INSERT_OSOBA]    Script Date: 4/10/2021 1:21:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PROC_INSERT_OSOBA]

 @OsobaId as INTEGER,
 @MaticniBroj VARCHAR(13),
 @Ime NVARCHAR(50), 
 @Prezime NVARCHAR(50),
 @Visina INT = null, 
 @Tezina INTEGER = null,
 @BojaOciju INTEGER = null,
 @Telefon VARCHAR(20) = null,
 @Email VARCHAR(50),
 @DatumRodjenja DATE,
 @Adresa NVARCHAR(50) = null,
 @Napomena NVARCHAR(50) = null,
 @MestoId INTEGER
 
AS  
	IF exists(SELECT 1 FROM Mesto WHERE MestoId = @MestoId)
BEGIN  
 INSERT INTO Osoba VALUES (@OsobaId, @MaticniBroj, @Ime, @Prezime, @Visina, @Tezina, @BojaOciju, @Telefon, @Email, @DatumRodjenja, @Adresa, @Napomena, @MestoId)  
END
GO
/****** Object:  Trigger [dbo].[TriggerVelikoSlovo]    Script Date: 4/10/2021 1:21:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TriggerVelikoSlovo]
	ON [dbo].[Mesto]
	AFTER UPDATE, INSERT
AS
BEGIN
UPDATE dbo.Mesto
SET Naziv = UPPER(LEFT(I.Naziv,1))+LOWER(SUBSTRING(I.Naziv,2,LEN(I.Naziv)))
FROM Mesto M,INSERTED I
WHERE M.MestoId = I.MestoId
END
GO
ALTER TABLE [dbo].[Mesto] ENABLE TRIGGER [TriggerVelikoSlovo]
GO
/****** Object:  Trigger [dbo].[TriggerImePrezime]    Script Date: 4/10/2021 1:21:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TriggerImePrezime]
	ON [dbo].[Osoba]
	AFTER UPDATE, INSERT
AS
BEGIN
UPDATE dbo.Osoba
SET Ime = UPPER(LEFT(I.Ime,1))+LOWER(SUBSTRING(I.Ime,2,LEN(I.Ime))),
Prezime = UPPER(LEFT(I.Prezime,1))+LOWER(SUBSTRING(I.Prezime,2,LEN(I.Prezime)))
FROM Osoba O,INSERTED I
WHERE O.OsobaId = I.OsobaId
END
GO
ALTER TABLE [dbo].[Osoba] ENABLE TRIGGER [TriggerImePrezime]
GO
USE [master]
GO
ALTER DATABASE [Baza] SET  READ_WRITE 
GO
