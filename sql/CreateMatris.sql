USE [SU_Casino]
GO

/****** Object:  Table [dbo].[matris]    Script Date: 2018-07-23 09:22:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[matris](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[prop_n] [nchar](10) NULL,
	[condition] [varchar](50) NULL,
	[moment] [int] NULL,
	[name] [varchar](50) NULL,
	[prob_S0] [int] NULL,
	[perc_S1] [int] NULL,
	[perc_S2] [int] NULL,
	[perc_S3] [int] NULL,
	[perc_S4] [int] NULL,
	[bet_R1] [int] NULL,
	[bet_R2] [int] NULL,
	[prob_O1] [int] NULL,
	[prob_O2] [int] NULL,
	[win_O1] [int] NULL,
	[win_O2] [int] NULL,
	[ifS0] [int] NULL,
	[ifS1win] [int] NULL,
	[ifS2win] [int] NULL,
	[ifS3win] [int] NULL,
	[ifS4win] [int] NULL,
	[ifS1probX] [int] NULL,
	[ifS2probX] [int] NULL,
	[ifS3probX] [int] NULL,
	[ifS4probX] [int] NULL,
	[hide] [varchar](50) NULL
) ON [PRIMARY]
GO


