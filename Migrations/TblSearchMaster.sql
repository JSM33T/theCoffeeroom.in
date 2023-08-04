USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblSearchMaster]    Script Date: 31-07-2023 16:28:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblSearchMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PermissionLevel] [nvarchar](10) NOT NULL,
	[Type] [varchar](20) NULL
) ON [PRIMARY]
GO

