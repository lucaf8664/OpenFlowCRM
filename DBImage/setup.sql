USE [master]
GO
/****** Object:  Database [MainDB]    Script Date: 06/07/2023 15:57:10 ******/
CREATE DATABASE [MainDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MainDB', FILENAME = N'/var/opt/mssql/data/MainDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ),
( NAME = N'MainDBFile', FILENAME = N'/var/opt/mssql/data/MainDBFile.ndf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MainDB_log', FILENAME = N'/var/opt/mssql/data/MainDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MainDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MainDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MainDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MainDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MainDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MainDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MainDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MainDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MainDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MainDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MainDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MainDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MainDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MainDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MainDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MainDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MainDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MainDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MainDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MainDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MainDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MainDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MainDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MainDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MainDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MainDB] SET  MULTI_USER 
GO
ALTER DATABASE [MainDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MainDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MainDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MainDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MainDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MainDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MainDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [MainDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MainDB]
GO
/****** Object:  Table [dbo].[Pezzi]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pezzi](
	[LottiID] [bigint] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Pezzi] PRIMARY KEY CLUSTERED 
(
	[LottiID] ASC,
	[TimeStamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PezziAggregatiOra]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PezziAggregatiOra]
AS
SELECT        TOP (100) PERCENT DATEADD(HOUR, DATEDIFF(HOUR, '2000', TimeStamp), '2000') AS date_truncated, COUNT(*) AS records_in_interval
FROM            dbo.Pezzi AS p
GROUP BY DATEDIFF(HOUR, '2000', TimeStamp)
ORDER BY date_truncated
GO
/****** Object:  View [dbo].[PezziAggregatiGiorno]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PezziAggregatiGiorno]
AS
SELECT        TOP (100) PERCENT DATEADD(DAY, DATEDIFF(DAY, '2000', TimeStamp), '2000') AS date_truncated, COUNT(*) AS records_in_interval
FROM            dbo.Pezzi AS p
GROUP BY DATEDIFF(DAY, '2000', TimeStamp)
ORDER BY date_truncated
GO
/****** Object:  View [dbo].[PezziAggregatiMinuto]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PezziAggregatiMinuto]
AS
SELECT        TOP (100) PERCENT DATEADD(MINUTE, DATEDIFF(MINUTE, '2000', TimeStamp), '2000') AS date_truncated, COUNT(*) AS records_in_interval
FROM            dbo.Pezzi AS p
GROUP BY DATEDIFF(MINUTE, '2000', TimeStamp)
ORDER BY date_truncated
GO
/****** Object:  Table [dbo].[CatalogoModelli]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogoModelli](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](50) NOT NULL,
	[Lunghezza] [int] NULL,
	[Larghezza] [int] NULL,
	[Altezza] [int] NULL,
 CONSTRAINT [PK_CatalogoModelli] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComponentiMerce]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentiMerce](
	[IDComponenteMerce] [bigint] IDENTITY(1,1) NOT NULL,
	[TipoComponente] [nvarchar](50) NOT NULL,
	[MaterialeComponente] [nvarchar](50) NOT NULL,
	[Lunghezza] [int] NULL,
	[Larghezza] [int] NULL,
	[Altezza] [int] NULL,
	[Note] [nvarchar](140) NULL,
 CONSTRAINT [PK_ComponentiMerce] PRIMARY KEY CLUSTERED 
(
	[IDComponenteMerce] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComponenteMerceCatalogoModelli]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponenteMerceCatalogoModelli](
	[ComponenteMerceId] [bigint] NOT NULL,
	[CatalogoModelliId] [bigint] NOT NULL,
	[Quantita] [int] NOT NULL,
 CONSTRAINT [PK_ComponenteMerceCatalogoModelli] PRIMARY KEY CLUSTERED 
(
	[ComponenteMerceId] ASC,
	[CatalogoModelliId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partite]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partite](
	[PartiteID] [bigint] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](50) NOT NULL,
	[PezziAlCarico] [int] NOT NULL,
	[NCarichi] [int] NOT NULL,
	[DataConsegna] [datetime] NOT NULL,
	[Modello] [bigint] NOT NULL,
	[Ordine] [bigint] NULL,
	[Stato] [int] NOT NULL,
 CONSTRAINT [PK_Partite] PRIMARY KEY CLUSTERED 
(
	[PartiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_PianoRiordino]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_PianoRiordino]
AS
SELECT        p.DataConsegna AS DataConsegnaPartita, cm.TipoComponente, cm.MaterialeComponente, cm.Lunghezza, cm.Larghezza, cm.Altezza, SUM(p.NCarichi * p.PezziAlCarico * cmm.Quantita) AS TotalePezziRichiesti, 
                         CAST(cm.Lunghezza * cm.Larghezza * cm.Altezza * SUM(p.NCarichi * p.PezziAlCarico * cmm.Quantita) AS float) / CAST(1000000 AS float) AS MetriCubi
FROM            dbo.ComponenteMerceCatalogoModelli AS cmm INNER JOIN
                         dbo.ComponentiMerce AS cm ON cmm.ComponenteMerceId = cm.IDComponenteMerce INNER JOIN
                         dbo.CatalogoModelli AS mp ON cmm.CatalogoModelliId = mp.Id INNER JOIN
                         dbo.Partite AS p ON mp.Id = p.Modello
GROUP BY p.DataConsegna, cm.TipoComponente, cm.MaterialeComponente, cm.Lunghezza, cm.Larghezza, cm.Altezza
GO
/****** Object:  View [dbo].[vw_PianoRiordinoBase]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_PianoRiordinoBase]
AS
SELECT        p.DataConsegna AS DataConsegnaPartita, cm.TipoComponente, cm.MaterialeComponente, cm.Lunghezza, cm.Larghezza, cm.Altezza, SUM(p.NCarichi * p.PezziAlCarico * cmm.Quantita) AS TotalePezziRichiesti, 
                         CAST(cm.Lunghezza * cm.Larghezza * cm.Altezza * SUM(p.NCarichi * p.PezziAlCarico * cmm.Quantita) AS float) / CAST(1000000 AS float) AS MetriCubi
FROM            dbo.ComponenteMerceCatalogoModelli AS cmm INNER JOIN
                         dbo.ComponentiMerce AS cm ON cmm.ComponenteMerceId = cm.IDComponenteMerce INNER JOIN
                         dbo.CatalogoModelli AS mp ON cmm.CatalogoModelliId = mp.Id INNER JOIN
                         dbo.Partite AS p ON mp.Id = p.Modello
GROUP BY p.DataConsegna, cm.TipoComponente, cm.MaterialeComponente, cm.Lunghezza, cm.Larghezza, cm.Altezza
GO
/****** Object:  Table [dbo].[AssegamentiLavoratori]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssegamentiLavoratori](
	[MacchineID] [bigint] NOT NULL,
	[RisorseUmaneID] [bigint] NOT NULL,
 CONSTRAINT [AssegnamentiLavoratori_pk] PRIMARY KEY CLUSTERED 
(
	[MacchineID] ASC,
	[RisorseUmaneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssegamentiLotti]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssegamentiLotti](
	[MacchineID] [bigint] NOT NULL,
	[LottiID] [bigint] NOT NULL,
 CONSTRAINT [AssegnamentiLotti_pk] PRIMARY KEY CLUSTERED 
(
	[MacchineID] ASC,
	[LottiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CambiMacchina]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CambiMacchina](
	[CambiMacchinaID] [bigint] IDENTITY(1,1) NOT NULL,
	[LottoIniziale] [bigint] NOT NULL,
	[LottoFinale] [bigint] NOT NULL,
	[Macchina] [bigint] NOT NULL,
	[TempoStimato] [time](7) NOT NULL,
	[Descrizione] [nvarchar](max) NULL,
 CONSTRAINT [PK_CambiMacchina] PRIMARY KEY CLUSTERED 
(
	[CambiMacchinaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carichi]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carichi](
	[IDCarico] [bigint] IDENTITY(1,1) NOT NULL,
	[DataConsegna] [datetime] NOT NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Carichi] PRIMARY KEY CLUSTERED 
(
	[IDCarico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clienti]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clienti](
	[IDCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Indirizzo] [nvarchar](150) NULL,
	[Note] [nvarchar](50) NULL,
 CONSTRAINT [PK_Clienti] PRIMARY KEY CLUSTERED 
(
	[IDCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComponentiMeccanici]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentiMeccanici](
	[ComponenteMeccanicoID] [bigint] NOT NULL,
	[Descrizione] [nvarchar](50) NOT NULL,
	[Macchina] [bigint] NOT NULL,
 CONSTRAINT [PK_ComponentiMeccanici] PRIMARY KEY CLUSTERED 
(
	[ComponenteMeccanicoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contatori]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contatori](
	[ContatoreID] [bigint] NOT NULL,
	[ComponenteMeccanico] [bigint] NOT NULL,
	[Soglia] [float] NOT NULL,
	[ContatoreCorrente] [float] NOT NULL,
	[UnitaMisura] [nvarchar](6) NOT NULL,
	[DataUltimoReset] [datetime] NULL,
 CONSTRAINT [PK_Contatori] PRIMARY KEY CLUSTERED 
(
	[ContatoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lotti]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lotti](
	[LottoID] [bigint] IDENTITY(0,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Lunghezza] [int] NOT NULL,
	[Larghezza] [int] NOT NULL,
	[Altezza] [int] NOT NULL,
	[Quantita] [int] NOT NULL,
	[TipoMateriale] [nvarchar](50) NULL,
	[Stato] [int] NOT NULL,
	[DataInizioPrevista] [datetime] NULL,
	[DataFinePrevista] [datetime] NULL,
	[DataInizioEffettiva] [datetime] NULL,
	[DataFineEffettiva] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[MacchinaAssegnata] [bigint] NULL,
 CONSTRAINT [PK_Lotti] PRIMARY KEY CLUSTERED 
(
	[LottoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Macchine]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Macchine](
	[MacchineID] [bigint] NOT NULL,
	[LottoCorrente] [bigint] NOT NULL,
	[Descrizione] [nvarchar](max) NULL,
	[Name] [nvarchar](50) NULL,
	[IP] [nvarchar](50) NULL,
	[RisorseUmane] [bigint] NULL,
	[X] [int] NOT NULL,
	[Y] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
 CONSTRAINT [PK_Macchine] PRIMARY KEY CLUSTERED 
(
	[MacchineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ordini]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordini](
	[OrdiniID] [bigint] IDENTITY(1,1) NOT NULL,
	[Descrizione] [nvarchar](max) NULL,
	[Cliente] [bigint] NOT NULL,
 CONSTRAINT [PK_Ordini] PRIMARY KEY CLUSTERED 
(
	[OrdiniID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RisorseUmane]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RisorseUmane](
	[RisorseUmaneID] [bigint] NOT NULL,
	[Cognome] [nvarchar](50) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RisorseUmane] PRIMARY KEY CLUSTERED 
(
	[RisorseUmaneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScomposizioneOrdiniPartite]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScomposizioneOrdiniPartite](
	[OrdiniID] [bigint] NOT NULL,
	[PartiteID] [bigint] NOT NULL,
 CONSTRAINT [ScomposizioneOrdiniLotti_pk] PRIMARY KEY CLUSTERED 
(
	[OrdiniID] ASC,
	[PartiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utenti]    Script Date: 06/07/2023 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utenti](
	[Id] [bigint] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Utenti] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Utenti] ([Id], [Username], [PasswordHash]) VALUES (0, N'admin', N'v8ziwZyFY/1Kpm9uxgc0H/JeX2/n+lINfRJC2HE4XyOj6OgAkxILSHfXlTXhCxgq4uyJN9H3LwkecXjJ5P8PEQ==')
GO
ALTER TABLE [dbo].[ComponenteMerceCatalogoModelli] ADD  CONSTRAINT [DF_ComponenteMerceCatalogoModelli_Quantita]  DEFAULT ((1)) FOR [Quantita]
GO
ALTER TABLE [dbo].[Ordini] ADD  CONSTRAINT [DF_Ordini_Cliente]  DEFAULT ((0)) FOR [Cliente]
GO
ALTER TABLE [dbo].[Partite] ADD  CONSTRAINT [DF_Partite_Modello]  DEFAULT ((0)) FOR [Modello]
GO
ALTER TABLE [dbo].[Partite] ADD  CONSTRAINT [DF_Partite_Stato]  DEFAULT ((0)) FOR [Stato]
GO
ALTER TABLE [dbo].[AssegamentiLavoratori]  WITH CHECK ADD  CONSTRAINT [FK_Macchine_HR] FOREIGN KEY([MacchineID])
REFERENCES [dbo].[Macchine] ([MacchineID])
GO
ALTER TABLE [dbo].[AssegamentiLavoratori] CHECK CONSTRAINT [FK_Macchine_HR]
GO
ALTER TABLE [dbo].[AssegamentiLavoratori]  WITH CHECK ADD  CONSTRAINT [FK_RisorseUmane] FOREIGN KEY([RisorseUmaneID])
REFERENCES [dbo].[RisorseUmane] ([RisorseUmaneID])
GO
ALTER TABLE [dbo].[AssegamentiLavoratori] CHECK CONSTRAINT [FK_RisorseUmane]
GO
ALTER TABLE [dbo].[AssegamentiLotti]  WITH CHECK ADD  CONSTRAINT [FK_Lotti] FOREIGN KEY([LottiID])
REFERENCES [dbo].[Partite] ([PartiteID])
GO
ALTER TABLE [dbo].[AssegamentiLotti] CHECK CONSTRAINT [FK_Lotti]
GO
ALTER TABLE [dbo].[AssegamentiLotti]  WITH CHECK ADD  CONSTRAINT [FK_Macchine] FOREIGN KEY([MacchineID])
REFERENCES [dbo].[Macchine] ([MacchineID])
GO
ALTER TABLE [dbo].[AssegamentiLotti] CHECK CONSTRAINT [FK_Macchine]
GO
ALTER TABLE [dbo].[CambiMacchina]  WITH CHECK ADD  CONSTRAINT [FK_CambiMacchina_Lotti] FOREIGN KEY([LottoIniziale])
REFERENCES [dbo].[Partite] ([PartiteID])
GO
ALTER TABLE [dbo].[CambiMacchina] CHECK CONSTRAINT [FK_CambiMacchina_Lotti]
GO
ALTER TABLE [dbo].[CambiMacchina]  WITH CHECK ADD  CONSTRAINT [FK_CambiMacchina_Lotti1] FOREIGN KEY([LottoFinale])
REFERENCES [dbo].[Partite] ([PartiteID])
GO
ALTER TABLE [dbo].[CambiMacchina] CHECK CONSTRAINT [FK_CambiMacchina_Lotti1]
GO
ALTER TABLE [dbo].[ComponenteMerceCatalogoModelli]  WITH CHECK ADD  CONSTRAINT [FK_ComponenteMerceCatalogoModelli_CatalogoModelli] FOREIGN KEY([CatalogoModelliId])
REFERENCES [dbo].[CatalogoModelli] ([Id])
GO
ALTER TABLE [dbo].[ComponenteMerceCatalogoModelli] CHECK CONSTRAINT [FK_ComponenteMerceCatalogoModelli_CatalogoModelli]
GO
ALTER TABLE [dbo].[ComponenteMerceCatalogoModelli]  WITH CHECK ADD  CONSTRAINT [FK_ComponenteMerceCatalogoModelli_ComponentiMerce] FOREIGN KEY([ComponenteMerceId])
REFERENCES [dbo].[ComponentiMerce] ([IDComponenteMerce])
GO
ALTER TABLE [dbo].[ComponenteMerceCatalogoModelli] CHECK CONSTRAINT [FK_ComponenteMerceCatalogoModelli_ComponentiMerce]
GO
ALTER TABLE [dbo].[ComponentiMeccanici]  WITH CHECK ADD  CONSTRAINT [FK_ComponentiMeccanici_Macchine] FOREIGN KEY([Macchina])
REFERENCES [dbo].[Macchine] ([MacchineID])
GO
ALTER TABLE [dbo].[ComponentiMeccanici] CHECK CONSTRAINT [FK_ComponentiMeccanici_Macchine]
GO
ALTER TABLE [dbo].[Contatori]  WITH CHECK ADD  CONSTRAINT [FK_Contatori_Contatori] FOREIGN KEY([ComponenteMeccanico])
REFERENCES [dbo].[ComponentiMeccanici] ([ComponenteMeccanicoID])
GO
ALTER TABLE [dbo].[Contatori] CHECK CONSTRAINT [FK_Contatori_Contatori]
GO
ALTER TABLE [dbo].[Lotti]  WITH CHECK ADD  CONSTRAINT [FK_Lotti_Macchine] FOREIGN KEY([MacchinaAssegnata])
REFERENCES [dbo].[Macchine] ([MacchineID])
GO
ALTER TABLE [dbo].[Lotti] CHECK CONSTRAINT [FK_Lotti_Macchine]
GO
ALTER TABLE [dbo].[Macchine]  WITH CHECK ADD  CONSTRAINT [FK_Macchine_LottoCorrente] FOREIGN KEY([LottoCorrente])
REFERENCES [dbo].[Partite] ([PartiteID])
GO
ALTER TABLE [dbo].[Macchine] CHECK CONSTRAINT [FK_Macchine_LottoCorrente]
GO
ALTER TABLE [dbo].[Ordini]  WITH CHECK ADD  CONSTRAINT [FK_Ordini_Clienti] FOREIGN KEY([Cliente])
REFERENCES [dbo].[Clienti] ([IDCliente])
GO
ALTER TABLE [dbo].[Ordini] CHECK CONSTRAINT [FK_Ordini_Clienti]
GO
ALTER TABLE [dbo].[Partite]  WITH CHECK ADD  CONSTRAINT [FK_Ordini_Partite] FOREIGN KEY([Ordine])
REFERENCES [dbo].[Ordini] ([OrdiniID])
GO
ALTER TABLE [dbo].[Partite] CHECK CONSTRAINT [FK_Ordini_Partite]
GO
ALTER TABLE [dbo].[Partite]  WITH CHECK ADD  CONSTRAINT [FK_Partite_Modelli] FOREIGN KEY([Modello])
REFERENCES [dbo].[CatalogoModelli] ([Id])
GO
ALTER TABLE [dbo].[Partite] CHECK CONSTRAINT [FK_Partite_Modelli]
GO
ALTER TABLE [dbo].[Pezzi]  WITH CHECK ADD  CONSTRAINT [FK_PezziLotti] FOREIGN KEY([LottiID])
REFERENCES [dbo].[Partite] ([PartiteID])
GO
ALTER TABLE [dbo].[Pezzi] CHECK CONSTRAINT [FK_PezziLotti]
GO
ALTER TABLE [dbo].[ScomposizioneOrdiniPartite]  WITH CHECK ADD  CONSTRAINT [FK_ScomposizioneLotti] FOREIGN KEY([PartiteID])
REFERENCES [dbo].[Partite] ([PartiteID])
GO
ALTER TABLE [dbo].[ScomposizioneOrdiniPartite] CHECK CONSTRAINT [FK_ScomposizioneLotti]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PezziAggregatiGiorno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PezziAggregatiGiorno'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 5610
         Alias = 900
         Table = 1170
         Output = 1515
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PezziAggregatiMinuto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PezziAggregatiMinuto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PezziAggregatiOra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PezziAggregatiOra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cmm"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cm"
            Begin Extent = 
               Top = 120
               Left = 38
               Bottom = 250
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mp"
            Begin Extent = 
               Top = 252
               Left = 38
               Bottom = 382
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 252
               Left = 262
               Bottom = 382
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_PianoRiordinoBase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_PianoRiordinoBase'
GO
USE [master]
GO
ALTER DATABASE [MainDB] SET  READ_WRITE 
GO
