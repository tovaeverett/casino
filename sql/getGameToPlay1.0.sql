USE [SU_Casino]
GO
/****** Object:  StoredProcedure [dbo].[getGameToPlay]    Script Date: 2018-08-29 13:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create  or alter PROCEDURE [dbo].[getGameToPlay]
	-- Add the parameters for the stored procedure here
	@seq int,
	@condition varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		trim(name) as name,
		trials,
		saldo,
		bet_R1,
		bet_R2,
		prob_O1,
		prob_O2,
		win_O1,
		win_O2,
		ifS1probX,
		ifS2probX,
		perc_S1,
		perc_S2,
		perc_S3,
		perc_S4,
		trim(S1_variant) as S1_variant,
		bet_R3,
		bet_B4,
		trim(ifS1win) as ifS1win,
		trim(ifS2win) as ifS2win,
		trim(ifS3win) as ifS3win,
		trim(ifS4win) as ifS4win,
		trim(if_R1) as if_R1,
		trim(if_R2) as if_R2,
		trim(if_R3) as if_R3,
		trim(if_R4) as if_R4,
		CloseToWinStep,
		CloseToWinColour,
		trim(InfoTextType) as InfoTextType,
		trim(JackpotTextType) as JackpotTextType,
		JackpotTime,
		trim(BannerTextType) as BannerTextType,
		Multiplyer,
		SpinnDelay1,
		SpinnDelay2
	FROM 
		matris
	WHERE 
		seq = @seq and condition = @condition

END