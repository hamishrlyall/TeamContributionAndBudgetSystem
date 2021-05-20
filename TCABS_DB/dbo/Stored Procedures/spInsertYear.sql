CREATE PROCEDURE [dbo].[spInsertYear]
   @Year INT
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Year] ( [Year] )
   VALUES( @Year )
END;
