CREATE PROCEDURE [dbo].[spGetPermissionsFromUsername]
	@Username NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	-- Get permission data
	SELECT p.[PermissionId], p.[PermissionName], p.[LinkTitle], p.[LinkPage], p.[LinkController]
	FROM [Permission] p
	INNER JOIN [RolePermission] r
	ON p.[PermissionId] = r.[PermissionId]
	WHERE r.[RoleId] IN (

		-- Get list of role IDs from user ID
		SELECT [RoleId] FROM [UserRole] WHERE [UserId] = (

			-- Get user ID from username
			SELECT [UserId] FROM [User] WHERE [Username] = @Username
		)
	)

END;
