USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblAwardMaster]    Script Date: 01-09-2023 17:18:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblAwardMaster](
	[Id] [int] NOT NULL,
	[Titile] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[Icon] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_AwardMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

