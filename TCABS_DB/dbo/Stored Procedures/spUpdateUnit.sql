CREATE PROCEDURE [dbo].[spUpdateUnit]
   @unitid INT,
   @name NVARCHAR(50)
AS
BEGIN
   UPDATE [dbo].[Unit]
   SET [Name] = @name
   WHERE [UnitId] = @unitid;
END;
