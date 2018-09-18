USE [SU_Casino]
GO

/****** Object:  Table [dbo].[playerLog]    Script Date: 2018-08-09 11:37:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[eventLog](
	[id] int identity,
	[user_Id] [varchar](50) NULL,
	[logDate] [datetime] NULL,
	[title] [varchar](100) NULL,
	[message] [varchar](max) NULL
) ON [PRIMARY]
GO


