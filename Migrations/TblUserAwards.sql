USE[coffeeroomdb]
GO

/****** Object:  Table [dbo].[TblUserAwards]    Script Date: 01-09-2023 17:17:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblUserAwards](
    [Id][int] NOT NULL,
    [UserId][int] NOT NULL,
[AwardId][int] NOT NULL,
    [DateReceived][datetime] NULL,
 CONSTRAINT[PK_TblUserAwards] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
) ON[PRIMARY]
GO

