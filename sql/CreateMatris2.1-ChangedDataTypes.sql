USE [SU_Casino]
GO

/****** Object:  Table [dbo].[matris]    Script Date: 2018-07-24 15:07:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create TABLE [dbo].[matris]
(
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[prop_n] [nchar](10) NULL,
	[condition] [varchar](50) NULL,
	[seq] [int] NULL,
	[trials] [int] NULL,
	[name] [varchar](50) NULL,
	[saldo] [int] NULL,
	[perc_S0] [int] NULL,
	[perc_S1] [int] NULL,
	[S1_variant] [varchar](50) NULL,
	[prob_S0] [int] NULL,
	[perc_S2] [int] NULL,
	[perc_S3] [int] NULL,
	[perc_S4] [int] NULL,
	[bet_R1] [int] NULL,
	[bet_R2] [int] NULL,
	[bet_R3] [int] NULL,
	[bet_B4] [int] NULL,
	[if_R1] [varchar](50) NULL,
	[if_R2] [varchar](50) NULL,
	[if_R3] [varchar](50) NULL,
	[if_R4] [varchar](50) NULL,
	[prob_O1] [int] NULL,
	[prob_O2] [int] NULL,
	[win_O1] [int] NULL,
	[win_O2] [int] NULL,
	[ifS1win] [varchar](50) NULL,
	[ifS2win] [varchar](50) NULL,
	[ifS3win] [varchar](50) NULL,
	[ifS4win] [varchar](50) NULL,
	[ifS1probX] [int] NULL,
	[ifS2probX] [int] NULL,
	[hide] [varchar](50) NULL,
	[freeze_win] [varchar](50) NULL
) ON [PRIMARY]
GO	
