USE [SU_Casino]
GO
/****** Object:  StoredProcedure [dbo].[insertQuestionsLog]    Script Date: 2018-07-27 08:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create or alter PROCEDURE [dbo].[insertQuestionsLog]
	-- Add the parameters for the stored procedure here
	@userid varchar(50),
	@q1 varchar(50),
	@q2 varchar(50),
	@q3 varchar(50),
	@q4 varchar(50),
	@q5 varchar(50),
	@q6 varchar(50),
	@q7 varchar(50),
	@q8 varchar(50),
	@q9 varchar(50),
	@q10 varchar(50),
	@q11 varchar(50),
	@q12 varchar(max),
	@q13 varchar(50),
	@Date datetime,
	@Device varchar(50),
	@Country varchar(50),
	@SurveyCode varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[QuestionsLog]
       (
		   userId
			,q1
			,q2
			,q3
			,q4
			,q5
			,q6
			,q7
			,q8
			,q9
			,q10
			,q11
			,q12
			,q13
			,Date
			,Device
			,Country
			,SurveyCode
	 )
     VALUES
	 (	
		@userid
		,@q1
		,@q2
		,@q3
		,@q4
		,@q5
		,@q6
		,@q7
		,@q8
		,@q9
		,@q10
		,@q11
		,@q12
		,@q13
		,@Date
		,@Device
		,@Country
		,@SurveyCode
	)
END
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
