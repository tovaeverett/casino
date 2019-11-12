USE [SU_Casino]
GO
/****** Object:  StoredProcedure [dbo].[getMatris]    Script Date: 2019-11-01 15:44:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Mattias Hugosson
-- Create date: 20180530
-- Description:	Select all document by assigenduserid.
-- =============================================
ALTER PROCEDURE [dbo].[getMatris]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		REPLACE(trim(prop_n),'.',',') as prop_n,
		trim(condition) as condition,
		seq,
		trials,
		trim(name) as name,
		saldo,
		prob_S0,
		perc_S0,
		perc_S1,
		trim(S1_variant) as S1_variant,
		perc_S2,
		perc_S3,
		perc_S4,
		bet_R1,
		bet_R2,
		bet_R3,
		bet_B4,
		trim(if_R1) as if_R1,
		trim(if_R2) as if_R2,
		trim(if_R3) as if_R3,
		trim(if_R4) as if_R4,
		prob_O1,
		prob_O2,
		win_O1,
		win_O2,
		trim(ifS1win) as ifS1win,
		trim(ifS2win) as ifS2win,
		trim(ifS3win) as ifS3win,
		trim(ifS4win) as ifS4win,
		ifS1probX,
		ifS2probX,
		trim(hide) as hide,
		trim(freeze_win) as freeze_win,
		RowID,
		CloseToWinStep,
		CloseToWinColour,
		trim(InfoTextType) as InfoTextType,
		trim(JackpotTextType) as JackpotTextType,
		JackpotTime,
		trim(BannerTextType) as BannerTextType,
		Multiplier,
		SpinDelay1,
		SpinDelay2
	FROM 
		matris 
	ORDER BY 
		condition, 
		seq;
END
