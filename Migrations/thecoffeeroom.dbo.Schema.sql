/****** USE <dbname>******/
GO
/****** Object:  Table [dbo].[TblAvatarMaster]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblAvatarMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](20) NULL,
	[Image] [nvarchar](20) NULL,
	[Description] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_TblAvatarMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblAwardMaster]    Script Date: 01-09-2023 17:02:24 ******/
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
/****** Object:  Table [dbo].[TblBlogAuthor]    Script Date: 01-09-2023 17:02:24 ******/
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
/****** Object:  Table [dbo].[TblBlogCategory]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBlogCategory](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](200) NULL,
	[AddedById] [nvarchar](4) NULL,
	[IsActive] [bit] NOT NULL,
	[Locator] [nvarchar](100) NULL,
 CONSTRAINT [PK_TblBlogCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblBlogComment]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBlogComment](
	[Id] [int] NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [nvarchar](400) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblBlogMaster]    Script Date: 01-09-2023 17:02:24 ******/
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
/****** Object:  Table [dbo].[TblBlogReply]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBlogReply](
	[Id] [int] NOT NULL,
	[CommentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Reply] [nvarchar](400) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblLogMaster]    Script Date: 01-09-2023 17:02:24 ******/
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
/****** Object:  Table [dbo].[TblMailingList]    Script Date: 01-09-2023 17:02:24 ******/
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
/****** Object:  Table [dbo].[TblNotification]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblNotification](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](20) NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Link] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_TblNotification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblPasswordReset]    Script Date: 01-09-2023 17:02:24 ******/
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
/****** Object:  Table [dbo].[TblSearchMaster]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSearchMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PermissionLevel] [nvarchar](10) NOT NULL,
	[Type] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblThemeMaster]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblThemeMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[String] [nvarchar](max) NOT NULL,
	[FontLink] [nvarchar](150) NOT NULL,
	[FontFamily] [nvarchar](50) NOT NULL,
	[CuratorId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CoverImage] [nvarchar](50) NOT NULL,
	[PrimaryCol] [nvarchar](10) NULL,
	[SecondaryCol] [nvarchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUpdateMaster]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUpdateMaster](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Link] [nvarchar](200) NULL,
	[DateAdded] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TblUpdateMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUserAwards]    Script Date: 01-09-2023 17:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUserAwards](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AwardId] [int] NOT NULL,
	[DateReceived] [datetime] NULL,
 CONSTRAINT [PK_TblUserAwards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUserProfile]    Script Date: 01-09-2023 17:02:24 ******/
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
	[DateJoined] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[Role] [nvarchar](50) NULL,
	[Bio] [nvarchar](200) NULL,
	[AvatarId] [int] NULL,
	[OTP] [nvarchar](6) NULL,
	[OTPTime] [datetime] NULL,
	[IsVerified] [bit] NULL,
	[CryptedPassword] [nvarchar](200) NULL,
 CONSTRAINT [PK_TblUserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
