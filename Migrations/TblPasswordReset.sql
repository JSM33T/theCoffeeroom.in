USE [coffeeroomdb]
GO
/****** Object:  Table [dbo].[TblPasswordReset]    Script Date: 09-08-2023 09:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPasswordReset](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[IsValid] [bit] NULL
) ON [PRIMARY]
GO
