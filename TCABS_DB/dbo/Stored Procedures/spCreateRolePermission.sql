create procedure spCreateRolePermission
@permissionid int,
@roleid int,
@rolepermissionid int out
AS
BEGIN

	DECLARE @count INT;

	SELECT @count = COUNT(*)
	FROM [dbo].[RolePermission]
	WHERE RoleId = @roleid AND PermissionId = @permissionid;

	IF (@count > 0)
	BEGIN

		SELECT TOP 1 @rolepermissionid = RolePermissionId
		FROM [dbo].[RolePermission]
		WHERE RoleId = @roleid AND PermissionId = @permissionid;

	END
	ELSE
	BEGIN

	   insert into RolePermission( RoleId, PermissionId )
	   values( @roleid, @permissionid )

	   SET @rolepermissionid = scope_identity();

   END;

   SELECT * FROM [dbo].[RolePermission]
   WHERE [RolePermissionId] = @rolepermissionid;

END;