USE [SU_Casino]
GO
/****** Object:  Table [dbo].[matris]    Script Date: 2018-08-09 11:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Chans för nära vinst ska vara konfigurebar i Adminpage.
ALTER TABLE [dbo].[matris]
ADD [CloseToWinStep] [int] NULL;
GO

-- Konfigurerbar resultet för nära vinst med samma färge (boolean) och siffror intervall i Admin.
ALTER TABLE [dbo].[matris]
ADD [CloseToWinColour] [bit] NULL;
GO

-- Möjlighet att själv kunna skapa och radera varianter av välkomstsmeddelanden, som märks med siffror
ALTER TABLE [dbo].[matris]
ADD [InfoTextType] [varchar](50) NULL;
GO

-- Ny flashigare typ av ”välkomstmeddelande” som dyker upp efter det initiala, 
-- vars presentation (p, samt vilket) styrs av nya kolumner i designmatrisen
ALTER TABLE [dbo].[matris]
ADD [JackpotTextType] [varchar](50) NULL;
GO

ALTER TABLE [dbo].[matris]
ADD [JackpotTime] [int] NULL;
GO
-- Möjlighet till kontinuerlig visning av ”erbjudandemeddelande” 
-- (minst fyra stycken alternativ i dropdown-meny) under spelandet i form av banner, 
-- styrs av nya kolumner i designmatrisen (p, samt vilken).
ALTER TABLE [dbo].[matris]
ADD [BannerTextType] [varchar](50) NULL;

-- Multiplier ska vara konfigurerbar i Adminpage.
ALTER TABLE [dbo].[matris]
ADD [Multiplier] [int] NULL;
GO

-- Nya kolumner i designmatrisen som styr relativ fördröjning av andra respektive tredje hjulets spinn 
ALTER TABLE [dbo].[matris]
ADD [SpinDelay1] [int] NULL;
GO

ALTER TABLE [dbo].[matris]
ADD [SpinDelay2] [int] NULL;
GO
-- Add index
ALTER TABLE [dbo].[matris]
ADD CONSTRAINT AK_matris UNIQUE(condition,seq);
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