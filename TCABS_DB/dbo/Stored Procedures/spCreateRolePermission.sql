create procedure spCreateRolePermission
@permissionid int,
@roleid int,
@rolepermissionid int out
AS
BEGIN

   insert into RolePermission( RoleId, PermissionId )
   values( @roleid, @permissionid )

   SET @rolepermissionid = scope_identity();
   SELECT * FROM [dbo].[RolePermission]
   WHERE [RolePermissionId] = @rolepermissionid;

END;