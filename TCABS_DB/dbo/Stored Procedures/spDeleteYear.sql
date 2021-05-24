CREATE PROCEDURE [dbo].[spDeleteYear]
   @yearid int
AS
BEGIN
   DELETE [Year]
   WHERE YearId = @yearid AND
   NOT EXISTS ( SELECT * FROM [dbo].[UnitOffering] WHERE [YearId] = @yearid );
END;
