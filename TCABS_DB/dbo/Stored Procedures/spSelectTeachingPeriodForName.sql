CREATE PROCEDURE [dbo].[spSelectTeachingPeriodForName]
   @name NVARCHAR(50)
AS
BEGIN
   SELECT * FROM [dbo].[TeachingPeriod]
   WHERE [Name] = @name
END;
