CREATE PROCEDURE [dbo].[spSelectUnitForUnitId]
   @unitid int
AS
BEGIN
   SELECT * FROM [dbo].[Unit]
   WHERE UnitId = @unitid
END;
