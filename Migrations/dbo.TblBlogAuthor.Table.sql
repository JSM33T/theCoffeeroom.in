USE [coffeeroomdb]
GO
/****** Object:  Table [dbo].[TblBlogAuthor]    Script Date: 09-08-2023 09:00:18 ******/
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
