CREATE PROCEDURE [dbo].[spDeleteUserRoles]   
@param1 int ,   @param2 int 
AS    
delete from RolePermission 
where roleid=@param1 and PermissionId=@param2 
RETURN 0 