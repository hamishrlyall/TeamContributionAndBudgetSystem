CREATE PROCEDURE [dbo].[spSelectPermission]
   @permissionid int
AS
BEGIN
   SELECT * FROM [dbo].[Permission]
   WHERE PermissionId = @permissionid
END;
