USE [coffeeroomdb]
GO
/****** Object:  Table [dbo].[TblLogMaster]    Script Date: 09-08-2023 09:00:18 ******/
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
