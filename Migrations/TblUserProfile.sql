USE [coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblUserProfile]    Script Date: 31-07-2023 16:29:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblUserProfile](
	[Id] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[EMail] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NULL,
	[Gender] [nvarchar](1) NULL,
	[Password] [nvarchar](20) NOT NULL,
	[DateJoined] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[Role] [nvarchar](50) NULL,
	[Bio] [varchar](200) NULL,
	[AvatarId] [int] NULL,
	[OTP] [nvarchar](6) NULL,
	[OTPTime] [datetime] NULL,
	[IsVerified] [bit] NULL,
 CONSTRAINT [PK_TblUserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

