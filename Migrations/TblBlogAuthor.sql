USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblBlogAuthor]    Script Date: 31-07-2023 16:27:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblBlogAuthor](
	[Id] [int] NOT NULL,
	[BlogId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL
) ON [PRIMARY]
GO

