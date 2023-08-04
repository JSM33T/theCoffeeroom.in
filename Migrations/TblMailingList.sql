USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblMailingList]    Script Date: 31-07-2023 16:28:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblMailingList](
	[Id] [int] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Origin] [nvarchar](max) NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_TblMailingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

