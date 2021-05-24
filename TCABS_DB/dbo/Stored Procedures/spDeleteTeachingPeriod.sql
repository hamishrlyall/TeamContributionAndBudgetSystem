CREATE PROCEDURE [dbo].[spDeleteTeachingPeriod]
   @teachingperiodid int
AS
BEGIN
   DELETE [TeachingPeriod]
   WHERE [TeachingPeriod].[TeachingPeriodId] = @teachingperiodid AND
   NOT EXISTS ( SELECT * FROM [dbo].[UnitOffering] WHERE [TeachingPeriodId] = @teachingperiodid );
END;
