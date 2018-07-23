USE [SU_Casino]
GO

/****** Object:  Table [dbo].[InfoText]    Script Date: 2018-07-23 09:23:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InfoText](
	[Text_Id] [int] NULL,
	[Text] [text] NULL,
	[Text_Name] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

