USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblBlogMaster]    Script Date: 31-07-2023 16:28:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblBlogMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[UrlHandle] [nvarchar](100) NOT NULL,
	[PostContent] [nvarchar](400) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Tags] [nvarchar](50) NOT NULL,
	[PostLike] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DatePosted] [datetime] NULL,
	[DateUpdated] [datetime] NULL
) ON [PRIMARY]
GO

