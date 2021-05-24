CREATE PROCEDURE [dbo].[spSelectUnitOfferingForDetails]
   @unitname NVARCHAR(50),
   @teachingperiodname NVARCHAR(50),
   @year INT
AS
BEGIN
   DECLARE @unitid INT
   SELECT @unitId = [UnitId]
   FROM [dbo].[Unit]
   WHERE [Name] = @unitname

   DECLARE @teachingperiodid INT
   SELECT @teachingperiodid = [TeachingPeriodId]
   FROM [dbo].[TeachingPeriod]
   WHERE [Name] = @teachingperiodname

   DECLARE @yearid INT
   SELECT @yearid = [YearId]
   FROM [dbo].[Year]
   WHERE [Year] = @year

   SELECT * FROM [dbo].[UnitOffering]
   WHERE [UnitId] = @unitid
   AND [TeachingPeriodId] = @teachingperiodid
   AND [YearId] = @yearid

END;
