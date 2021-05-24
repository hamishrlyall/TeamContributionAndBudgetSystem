CREATE PROCEDURE [dbo].[spUpdateTeachingPeriod]
   @teachingperiodid INT,
   @month INT,
   @day INT,
   @name NVARCHAR(50)
AS
BEGIN
   SELECT DATEFROMPARTS( 2000, @month, @day ) AS Result;

   UPDATE [dbo].[TeachingPeriod]
   SET [Name] = @name, 
       [Month] = @month, 
       [Day] = @day
   WHERE [TeachingPeriodId] = @teachingperiodid;
END;
