USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblLogMaster]    Script Date: 31-07-2023 16:28:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblLogMaster](
	[Id] [int] NOT NULL,
	[Source] [nvarchar](100) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[String] [nvarchar](500) NOT NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY]
GO

