CREATE PROCEDURE [dbo].[spInsertUnitOffering]
   @unitid int,
   @yearid int,
   @teachingperiodid int,
   @convenorid int,
   @unitofferingid int out
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @year INT
   SELECT @year = [Year]
   FROM [dbo].[Year] 
   WHERE [YearId] = @yearid

   DECLARE @month INT
   SELECT @month = [Month]
   FROM [dbo].[TeachingPeriod]
   WHERE[TeachingPeriodId] = @teachingperiodid

   DECLARE @day INT
   SELECT @day = [Day]
   FROM [dbo].[TeachingPeriod]
   WHERE[TeachingPeriodId] = @teachingperiodid

   SELECT DATEFROMPARTS( @year, @month, @day ) AS Result;

   INSERT INTO [dbo].[UnitOffering]([UnitId], [YearId], [TeachingPeriodId], [ConvenorId])
   SELECT @unitid, @yearid, @teachingperiodid, @convenorid
   WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[UnitOffering] WHERE [UnitId] = @unitid AND [YearId] = @yearid AND [TeachingPeriodId] = @teachingperiodid );

   SET @unitofferingid = scope_identity( );
   SELECT * FROM [dbo].[UnitOffering] WHERE [UnitOfferingId] = @unitofferingid;
END;
