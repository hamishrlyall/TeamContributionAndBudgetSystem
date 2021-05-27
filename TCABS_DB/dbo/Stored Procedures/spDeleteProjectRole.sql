CREATE PROCEDURE [dbo].[spDeleteProjectRole]
   @ProjectRoleId int
AS
BEGIN
	DECLARE @temp INT;

	-- Check if this ProjectRole is being used by a ProjectRoleLink
	SELECT @temp = COUNT(*) FROM [dbo].[ProjectRoleLink] WHERE ProjectRoleId = @ProjectRoleId;
	IF (@temp > 0)
		RAISERROR('Cannot delete project role while it is assigned to a role group', 16, 1);
	ELSE
	BEGIN
	
		-- Delete project role
		DELETE FROM [dbo].[ProjectRole]
		WHERE ProjectRoleId = @ProjectRoleId
	END;
END;
