USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblThemeMaster]    Script Date: 31-07-2023 16:29:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblThemeMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[String] [nvarchar](max) NOT NULL,
	[FontLink] [nvarchar](150) NOT NULL,
	[FontFamily] [nvarchar](50) NOT NULL,
	[CuratorId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CoverImage] [nvarchar](50) NOT NULL,
	[PrimaryCol] [nvarchar](10) NULL,
	[SecondaryCol] [nvarchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

