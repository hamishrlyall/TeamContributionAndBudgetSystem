CREATE PROCEDURE [dbo].[spInsertYear]
   @year INT,
   @yearid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Year] ( [Year] )
   VALUES( @year )
   
   SET @yearid = scope_identity( );
   SELECT * FROM [dbo].[Year] WHERE [YearId] = @yearid;

END;
