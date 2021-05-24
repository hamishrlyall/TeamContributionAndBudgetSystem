CREATE PROCEDURE [dbo].[spSelectUnitOfferingCountForTeachingPeriod]
   @teachingperiodid int
AS
BEGIN
   SELECT COUNT(*) FROM [UnitOffering]
   WHERE [TeachingPeriodId] = @teachingperiodid 
END;
