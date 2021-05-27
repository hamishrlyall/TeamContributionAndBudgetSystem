CREATE PROCEDURE [dbo].[spDeleteRolePermission]   
   @permissionid int,   
   @roleid int 
AS
BEGIN
   delete from RolePermission 
   where roleid=@roleid and PermissionId=@permissionid 
END; 