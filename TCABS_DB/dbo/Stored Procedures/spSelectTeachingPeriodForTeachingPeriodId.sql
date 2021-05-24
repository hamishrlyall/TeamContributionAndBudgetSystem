CREATE PROCEDURE [dbo].[spSelectTeachingPeriodForTeachingPeriodId]
   @teachingperiodid int
AS
BEGIN
   SELECT * FROM [dbo].[TeachingPeriod]
   WHERE TeachingPeriodId = @teachingperiodid
END
