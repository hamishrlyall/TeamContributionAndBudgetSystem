CREATE PROCEDURE [dbo].[spSelectTeachingPeriods]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[TeachingPeriod]
END;
