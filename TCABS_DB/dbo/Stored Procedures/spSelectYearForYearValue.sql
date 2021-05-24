CREATE PROCEDURE [dbo].[spSelectYearForYearValue]
   @year int
AS
BEGIN
   SELECT * FROM [dbo].[Year]
   WHERE [Year] = @year
END;
