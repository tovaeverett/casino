USE [SU_Casino]
GO

/****** Object:  StoredProcedure [dbo].[getPlayerCredit]    Script Date: 2018-07-23 09:25:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getPlayerCredit]
	-- Add the parameters for the stored procedure here
	@user_id varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT distinct balance_out from playerLog where user_id = @user_id
END
GO

