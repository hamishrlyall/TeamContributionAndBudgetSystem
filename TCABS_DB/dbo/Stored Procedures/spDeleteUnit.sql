CREATE PROCEDURE [dbo].[spDeleteUnit]
   @unitid int
AS
BEGIN
   DELETE [Unit]
   WHERE UnitId = @unitid AND
   NOT EXISTS ( SELECT * FROM [dbo].[UnitOffering] WHERE [UnitId] = @unitid );
END;
