USE [SU_Casino]
GO
/****** Object:  StoredProcedure [dbo].[getGameToPlay]    Script Date: 2018-08-16 14:46:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[getGameToPlay]
	-- Add the parameters for the stored procedure here
	@seq int,
	@condition varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT name, trials, saldo, Prob_S0 ,bet_R1, bet_R2, prob_O1, prob_O2, win_O1, win_O2, ifS1probX, ifS2probX from matris where seq = @seq and condition = @condition
END
