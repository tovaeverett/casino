USE [SU_Casino]
GO

/****** Object:  Table [dbo].[InfoText]    Script Date: 2018-07-23 09:23:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[InfoText_Backup]
GO

CREATE TABLE [dbo].[InfoText_Backup](
	[Text_Id] [int] NOT NULL,
	[Text] [text] NULL,
	[Text_Name] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when creating [dbo].[InfoText_Backup].';   
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful creating [dbo].[InfoText_Backup]';   
    END;  
GO 

INSERT INTO [dbo].[InfoText_Backup]
SELECT 
	[Text_Id],
	[Text],
	[Text_Name] 
FROM [dbo].[InfoText]
GO

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when backup data to [dbo].[InfoText_Backup].';   
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful backuping data to [dbo].[InfoText_Backup]';   
    END;  
GO 


DROP TABLE [dbo].[InfoText]
GO

CREATE TABLE [dbo].[InfoText](
	[Text_Id] [int] NOT NULL,
	[Text] [text] NULL,
	[Text_Name] [varchar](50) NOT NULL PRIMARY KEY,
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when dropping or creating table [dbo].[InfoText].';  
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful creating table [dbo].[InfoText]';  
    END;  
GO 


-- Fill in current text

INSERT INTO [dbo].[InfoText]
SELECT 
	[Text_Id],
	[Text],
	[Text_Name] +'1'
FROM [dbo].[InfoText_Backup]
WHERE [Text_Id] < 4
GO

INSERT INTO [dbo].[InfoText]
SELECT 
	[Text_Id],
	[Text],
	[Text_Name] 
FROM [dbo].[InfoText_Backup]
WHERE [Text_Id] >= 4
GO

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when dropping or creating table [dbo].[InfoText].';    
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful creating table [dbo].[InfoText]';   
    END;  
GO 

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (1, N'Welcome text 2 ROULETTE', N'playRouletteInfo2')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (1, N'Welcome text 3 ROULETTE', N'playRouletteInfo3')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (1, N'Welcome text 4 ROULETTE', N'playRouletteInfo4')

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (2, N'Welcome text 2 CARD DRAW', N'playCardInfo2')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (2, N'Welcome text 3 CARD DRAW', N'playCardInfo3')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (2, N'Welcome text 4 CARD DRAW', N'playCardInfo4')

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (3, N'Welcome text 2 SLOT', N'playSlotInfo2')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (3, N'Welcome text 3 SLOT', N'playSlotInfo3')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (3, N'Welcome text 4 SLOT', N'playSlotInfo4')

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (8, N'Jackpot text ROULETTE', N'jackpotRouletteInfo')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (9, N'Jackpot text CARD DRAW', N'jackpotCardInfo')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (10, N'Jackpot text SLOT', N'jackpotSlotInfo')

--INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (11, N'Banner text 1 ROULETTE', N'bannerRouletteInfo1')
--INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (11, N'Banner text 2 ROULETTE', N'bannerRouletteInfo2')
--INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (11, N'Banner text 3 ROULETTE', N'bannerRouletteInfo3')
--INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (11, N'Banner text 4 ROULETTE', N'bannerRouletteInfo4')

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (12, N'Banner text 1 CARD DRAW', N'bannerCardInfo1')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (12, N'Banner text 2 CARD DRAW', N'bannerCardInfo2')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (12, N'Banner text 3 CARD DRAW', N'bannerCardInfo3')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (12, N'Banner text 4 CARD DRAW', N'bannerCardInfo4')

INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (13, N'Banner text 1 SLOT', N'bannerSlotInfo1')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (13, N'Banner text 2 SLOT', N'bannerSlotInfo2')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (13, N'Banner text 3 SLOT', N'bannerSlotInfo3')
INSERT [dbo].[InfoText] ([Text_Id], [Text], [Text_Name]) VALUES (13, N'Banner text 4 SLOT', N'bannerSlotInfo4')


IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when inserting to table [dbo].[InfoText].';  
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful inserting to table [dbo].[InfoText]';   
    END;  
GO 
