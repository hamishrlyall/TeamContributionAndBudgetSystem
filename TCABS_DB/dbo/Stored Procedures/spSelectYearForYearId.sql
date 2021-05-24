CREATE PROCEDURE [dbo].[spSelectYearForYearId]
   @yearid int
AS
BEGIN
   SELECT * FROM [dbo].[Year]
   WHERE YearId = @yearid
END;
