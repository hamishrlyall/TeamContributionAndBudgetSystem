CREATE PROCEDURE [dbo].[spSelectUnitOfferingCountForUnit]
   @unitid int
AS
BEGIN
   SELECT COUNT(*) FROM [UnitOffering]
   WHERE [UnitId] = @unitid 
END;
