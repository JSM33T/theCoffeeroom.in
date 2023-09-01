USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblBlogLike]    Script Date: 01-09-2023 17:17:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblBlogLike](
	[Id] [int] NOT NULL,
	[BlogId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL
) ON [PRIMARY]
GO

