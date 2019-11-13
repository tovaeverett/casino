USE [SU_Casino]
GO
/****** Object:  Table [dbo].[matris]    Script Date: 2018-08-09 11:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[matris_backup]
GO

CREATE TABLE [dbo].[matris_backup](
	[prop_n] [nchar](10) NULL,
	[condition] [varchar](50) NULL,
	[seq] [int] NULL,
	[trials] [int] NULL,
	[name] [varchar](50) NULL,
	[saldo] [int] NULL,
	[prob_S0] [float] NULL,
	[perc_S0] [float] NULL,
	[perc_S1] [float] NULL,
	[S1_variant] [varchar](50) NULL,
	[perc_S2] [float] NULL,
	[perc_S3] [float] NULL,
	[perc_S4] [float] NULL,
	[bet_R1] [int] NULL,
	[bet_R2] [int] NULL,
	[bet_R3] [int] NULL,
	[bet_B4] [int] NULL,
	[if_R1] [varchar](50) NULL,
	[if_R2] [varchar](50) NULL,
	[if_R3] [varchar](50) NULL,
	[if_R4] [varchar](50) NULL,
	[prob_O1] [float] NULL,
	[prob_O2] [float] NULL,
	[win_O1] [int] NULL,
	[win_O2] [int] NULL,
	[ifS1win] [varchar](50) NULL,
	[ifS2win] [varchar](50) NULL,
	[ifS3win] [varchar](50) NULL,
	[ifS4win] [varchar](50) NULL,
	[ifS1probX] [int] NULL,
	[ifS2probX] [int] NULL,
	[hide] [varchar](50) NULL,
	[freeze_win] [varchar](50) NULL,
	[RowId] [int] NOT NULL
) ON [PRIMARY]
GO

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when creating table [dbo].[matris_backup].';  
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful creating table [dbo].[matris_backup]';  
    END;  
GO 

INSERT INTO [dbo].[matris_backup]
SELECT 
	[prop_n],
	[condition],
	[seq],
	[trials],
	[name],
	[saldo],
	[prob_S0],
	[perc_S0],
	[perc_S1],
	[S1_variant],
	[perc_S2],
	[perc_S3],
	[perc_S4],
	[bet_R1],
	[bet_R2],
	[bet_R3],
	[bet_B4],
	[if_R1],
	[if_R2],
	[if_R3],
	[if_R4],
	[prob_O1],
	[prob_O2],
	[win_O1],
	[win_O2],
	[ifS1win],
	[ifS2win],
	[ifS3win],
	[ifS4win],
	[ifS1probX],
	[ifS2probX],
	[hide],
	[freeze_win],
	[RowId]
FROM
	 [dbo].[matris]
GO


IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when inserting table [dbo].[matris_backup].'; 
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful inserting table [dbo].[matris_backup]';  
    END;  
GO 

DROP TABLE [dbo].[matris]
GO

CREATE TABLE [dbo].[matris](
	[prop_n] [nchar](10) NULL,
	[condition] [varchar](50) NOT NULL,
	[seq] [int] NOT NULL,
	[trials] [int] NULL,
	[name] [varchar](50) NULL,
	[saldo] [int] NULL,
	[prob_S0] [float] NULL,
	[perc_S0] [float] NULL,
	[perc_S1] [float] NULL,
	[S1_variant] [varchar](50) NULL,
	[perc_S2] [float] NULL,
	[perc_S3] [float] NULL,
	[perc_S4] [float] NULL,
	[bet_R1] [int] NULL,
	[bet_R2] [int] NULL,
	[bet_R3] [int] NULL,
	[bet_B4] [int] NULL,
	[if_R1] [varchar](50) NULL,
	[if_R2] [varchar](50) NULL,
	[if_R3] [varchar](50) NULL,
	[if_R4] [varchar](50) NULL,
	[prob_O1] [float] NULL,
	[prob_O2] [float] NULL,
	[win_O1] [int] NULL,
	[win_O2] [int] NULL,
	[ifS1win] [varchar](50) NULL,
	[ifS2win] [varchar](50) NULL,
	[ifS3win] [varchar](50) NULL,
	[ifS4win] [varchar](50) NULL,
	[ifS1probX] [int] NULL,
	[ifS2probX] [int] NULL,
	[hide] [varchar](50) NULL,
	[freeze_win] [varchar](50) NULL,
	[RowId] [int] IDENTITY(1,1) NOT NULL,
	[CloseToWinStep] [int] NULL,
	[CloseToWinColour] [bit] NULL,
	[InfoTextType] [varchar](50) NULL,
	[JackpotTextType] [varchar](50) NULL,
	[JackpotTime] [int] NULL,
	[BannerTextType] [varchar](50) NULL,
	[Multiplier] [int] NULL,
	[SpinDelay1] [int] NULL,
	[SpinDelay2] [int] NULL,
	CONSTRAINT AK_matris UNIQUE(condition,seq)
) ON [PRIMARY]
GO

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when creating table [dbo].[matris].';  
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful creating table [dbo].[matris]';  
    END;  
GO 

SET IDENTITY_INSERT [dbo].[matris] ON 
GO 

INSERT [dbo].[matris] 
	([prop_n], 
	[condition], 
	[seq], 
	[trials], 
	[name], 
	[saldo], 
	[prob_S0], 
	[perc_S0], 
	[perc_S1], 
	[S1_variant], 
	[perc_S2], 
	[perc_S3], 
	[perc_S4], 
	[bet_R1], 
	[bet_R2], 
	[bet_R3], 
	[bet_B4], 
	[if_R1], 
	[if_R2], 
	[if_R3], 
	[if_R4], 
	[prob_O1], 
	[prob_O2], 
	[win_O1], 
	[win_O2], 
	[ifS1win], 
	[ifS2win], 
	[ifS3win], 
	[ifS4win], 
	[ifS1probX], 
	[ifS2probX], 
	[hide], 
	[freeze_win], 
	[RowID], 
	[CloseToWinStep],
	[CloseToWinColour],
	[InfoTextType],
	[JackpotTextType],
	[JackpotTime],
	[BannerTextType],
	[Multiplier],
	[SpinDelay1],
	[SpinDelay2])
SELECT 
	[prop_n], 
	[condition], 
	[seq], 
	[trials], 
	[name], 
	[saldo], 
	[prob_S0], 
	[perc_S0], 
	[perc_S1], 
	[S1_variant], 
	[perc_S2], 
	[perc_S3], 
	[perc_S4], 
	[bet_R1], 
	[bet_R2], 
	[bet_R3], 
	[bet_B4], 
	[if_R1], 
	[if_R2], 
	[if_R3], 
	[if_R4], 
	[prob_O1], 
	[prob_O2], 
	[win_O1], 
	[win_O2], 
	[ifS1win], 
	[ifS2win], 
	[ifS3win], 
	[ifS4win], 
	[ifS1probX], 
	[ifS2probX], 
	[hide], 
	[freeze_win], 
	[RowID], 
	NULL,
	0,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL
FROM [dbo].[matris_backup]
GO 

IF @@ERROR <> 0   
    BEGIN  
        -- Return 99 to the calling program to indicate failure.  
        PRINT N'An error occurred when inserting table [dbo].[matris].';  
    END  
ELSE  
    BEGIN  
        -- Return 0 to the calling program to indicate success.  
        PRINT N'Successful inserting table [dbo].[matris]';  
    END;  
GO 

SET IDENTITY_INSERT [dbo].[matris] OFF

UPDATE [dbo].[matris]
SET [InfoTextType] = N'playCardInfo1'
WHERE [name] = N'Instrumental_acq'
GO
 
UPDATE [dbo].[matris]
SET [InfoTextType] = N'playSlotInfo1'
WHERE [name] = N'Pavlovian_acq'
GO
 
UPDATE [dbo].[matris]
SET [InfoTextType] = N'playRouletteInfo1'
WHERE [name] = N'Roulette'
GO