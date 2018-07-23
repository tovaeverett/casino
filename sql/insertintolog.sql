USE [SU_Casino]
GO

/****** Object:  StoredProcedure [dbo].[insertIntoLog]    Script Date: 2018-07-23 09:25:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertIntoLog]
	-- Add the parameters for the stored procedure here
	@userid varchar(50),
	@condition varchar(50),
	@gamename varchar(50),
	@moment int,
	@trial int,
	@balance_in int,
	@balance_out int,
	@stimuli varchar(50),
	@bet int,
	@outcome int,
	@response varchar(50),
	@timestamp_begin time(7),
	@timestamp_R time(7),
	@timestamp_O time(7)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[playerLog]
       (
		   user_Id
           ,condition
           ,game_name
           ,moment
           ,trial
           ,balance_in
		   ,balance_out
		   ,stimuli
		   ,bet
		   ,outcome
		   ,response
		   ,timestamp_begin
		   ,timestamp_O
		   ,timestamp_R
	 )
     VALUES
	 (	
		@userid,
		@condition,
		@gamename,
		@moment,
		@trial,
		@balance_in,
		@balance_out,
		@stimuli,
		@bet,
		@outcome,
		@response,
		@timestamp_begin,
		@timestamp_R,
		@timestamp_O
	)
END
GO

