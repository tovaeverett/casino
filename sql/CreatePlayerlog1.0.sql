USE [SU_Casino]
GO

/****** Object:  Table [dbo].[playerLog]    Script Date: 2019-11-04 10:28:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[playerLog]
GO

CREATE TABLE [dbo].[playerLog](
	[user_Id] [varchar](50) NULL,
	[condition] [varchar](50) NULL,
	[game_name] [varchar](50) NULL,
	[moment] [int] NULL,
	[trial] [int] NULL,
	[timestamp_begin] [datetime] NULL,
	[timestamp_R] [datetime] NULL,
	[timestamp_O] [datetime] NULL,
	[balance_in] [int] NULL,
	[response] [varchar](50) NULL,
	[bet] [int] NULL,
	[stimuli] [varchar](50) NULL,
	[outcome] [int] NULL,
	[balance_out] [int] NULL,
	[q_win_chance] [varchar](50) NULL
) ON [PRIMARY]
GO

