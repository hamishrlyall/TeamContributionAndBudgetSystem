CREATE PROCEDURE [dbo].[spInsertTeachingPeriod]
   @name NVARCHAR(50),
   @month int,
   @day int,
   @teachingperiodid int out
AS
BEGIN
   SET NOCOUNT ON;
   SELECT DATEFROMPARTS( 2000, @month, @day ) AS Result;

   INSERT INTO [dbo].[TeachingPeriod] ([Name], [Month], [Day])
   VALUES ( @name, @month, @day )

      SET @teachingperiodid = scope_identity( );
   SELECT * FROM [dbo].[TeachingPeriod] WHERE [TeachingPeriodId] = @teachingperiodid;
END;