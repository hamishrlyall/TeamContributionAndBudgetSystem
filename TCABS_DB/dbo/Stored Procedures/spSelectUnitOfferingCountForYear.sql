CREATE PROCEDURE [dbo].[spSelectUnitOfferingCountForYear]
   @yearid int
AS
BEGIN
   SELECT COUNT(*) FROM [UnitOffering]
   WHERE [YearId] = @yearid 
END;
