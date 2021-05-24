CREATE PROCEDURE [dbo].[spSelectUnitForName]
   @name NVARCHAR(50)
AS
BEGIN
   SELECT * FROM [dbo].[Unit]
   WHERE [Name] = @name
END;
