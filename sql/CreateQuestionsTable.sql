USE [SU_Casino]
GO

/****** Object:  Table [dbo].[QuestionsLog]    Script Date: 2018-07-27 08:52:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QuestionsLog](
	[userid] [varchar](50) NULL,
	[q1] [varchar](50) NULL,
	[q2] [varchar](50) NULL,
	[q3] [varchar](50) NULL,
	[q4] [varchar](50) NULL,
	[q5] [varchar](50) NULL,
	[q6] [varchar](50) NULL,
	[q7] [varchar](50) NULL,
	[q8] [varchar](50) NULL,
	[q9] [varchar](50) NULL,
	[q10] [varchar](50) NULL,
	[q11] [varchar](50) NULL,
	[q12] [varchar](max) NULL,
	[q13] [varchar](50) NULL,
	[Date] [time](7) NULL,
	[Device] [varchar](50) NULL,
	[Country] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


