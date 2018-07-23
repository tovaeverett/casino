USE [SU_Casino]
GO

/****** Object:  Table [dbo].[playerLog]    Script Date: 2018-07-23 09:23:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[playerLog](
	[user_Id] [varchar](50) NULL,
	[condition] [varchar](50) NULL,
	[game_name] [varchar](50) NULL,
	[moment] [int] NULL,
	[trial] [int] NULL,
	[timestamp_begin] [time](7) NULL,
	[timestamp_R] [time](7) NULL,
	[timestamp_O] [time](7) NULL,
	[balance_in] [int] NULL,
	[response] [varchar](50) NULL,
	[bet] [int] NULL,
	[stimuli] [varchar](50) NULL,
	[outcome] [int] NULL,
	[balance_out] [int] NULL
) ON [PRIMARY]
GO

