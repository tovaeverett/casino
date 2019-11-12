USE [SU_Casino]
GO

/****** Object:  StoredProcedure [dbo].[getLog]    Script Date: 2019-11-11 15:54:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[getLog]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 
	SELECT 
		TRIM(user_Id) as user_id,
		TRIM(condition) as condition,
		TRIM(game_name) as game_name,
		moment,
		trial,
		-- Converted to string to show correct format in excel
		CONVERT(char(23), timestamp_begin, 121) as timestamp_begin,
		CONVERT(char(23), timestamp_R, 121) as timestamp_R,
		CONVERT(char(23), timestamp_O, 121) as timestamp_O,
		balance_in,
		response,
		bet,
		TRIM(stimuli) as stimuli,
		outcome,
		balance_out,
		q_win_chance,
		figure_1,
		figure_2,
		figure_3
	FROM 
		playerLog
	ORDER BY 
		timestamp_begin;
END
GO

