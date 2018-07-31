USE [SU_Casino]
GO

/****** Object:  StoredProcedure [dbo].[getAllTheme]    Script Date: 2018-07-23 09:24:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAllTheme]
	-- Add the parameters for the stored procedure here
	@prop_n varchar(50),
	@moment int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [perc_S1],[perc_S2],[perc_S3],[perc_S4] from matris where prop_n = @prop_n and seq = @moment
END
GO

