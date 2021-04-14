CREATE PROCEDURE [dbo].[spSelectRolePermissionsForRoleId]
   @roleid int
AS
BEGIN
   SELECT r.[PermissionId]
   FROM [ Role ] r
   LEFT OUTER JOIN[ RolePermission ] rp ON rp.RoleId = r.RoleId
   LEFT OUTER JOIN[ Permission ] p ON p.PermissionId = rp.PermissionId
   WHERE r.RoleId = @roleid
END;