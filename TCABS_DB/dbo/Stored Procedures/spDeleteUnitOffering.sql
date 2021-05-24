CREATE PROCEDURE [dbo].[spDeleteUnitOffering]
   @unitofferingid int
AS
BEGIN
--BEGIN TRY

   --DECLARE @TempUnitOffering TABLE(
   --   YearId INT,
   --   TeachingPeriodId INT
   --);

   --INSERT @TempUnitOffering
   --SELECT YearId, TeachingPeriodId
   --FROM UnitOffering
   --WHERE UnitOfferingId = @unitofferingid

   --SELECT [UnitOfferingId],
   --       [YearId],
   --       [TeachingPeriodId]
   --INTO #tempunitoffering
   --FROM [dbo].[UnitOffering]
   --WHERE [UnitOfferingId] = @unitofferingid

   DECLARE @yearid INT
   SELECT @yearid = YearId
   FROM [UnitOffering]
   WHERE UnitOfferingId = @unitofferingid;

   DECLARE @teachingperiodid INT
   SELECT @teachingperiodid = TeachingPeriodId
   FROM [UnitOffering]
   WHERE UnitOfferingId = @unitofferingid;

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

   DECLARE @result DATETIME
   SELECT @result = DATEFROMPARTS( @year, @month, @day )
   
   DECLARE @currentdate DATETIME
   SELECT @currentdate = GETDATE( )

   IF( @result > @currentdate )
      DELETE [UnitOffering]
      WHERE [UnitOfferingId] = @unitofferingid
      AND NOT EXISTS ( SELECT * FROM [dbo].[Enrollment] WHERE [UnitOfferingId] = @unitofferingid );
END;
--   IF( @result < @currentdate )
--      DELETE [UnitOffering]
--      WHERE [UnitOfferingId] = @unitofferingid
--      AND NOT EXISTS ( SELECT * FROM [dbo].[Enrollment] WHERE [UnitOfferingId] = @unitofferingid );
--   ELSE
--      THROW 50001, 'Cannot DELETE Unit Offering which has already commenced', 1;
--END TRY
--BEGIN CATCH
--   SELECT 
--      ERROR_NUMBER() AS ErrorNumber,
--      ERROR_SEVERITY( ) AS ErrorSeverity,
--      ERROR_MESSAGE( ) AS ErrorMessage;
--END CATCH
