USE [SU_Casino]
GO

/****** Object:  StoredProcedure [dbo].[getMatris]    Script Date: 2018-07-23 09:24:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Mattias Hugosson
-- Create date: 20180530
-- Description:	Select all document by assigenduserid.
-- =============================================
Create PROCEDURE [dbo].[getMatris]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM matris ORDER BY condition, seq;
END
GO

