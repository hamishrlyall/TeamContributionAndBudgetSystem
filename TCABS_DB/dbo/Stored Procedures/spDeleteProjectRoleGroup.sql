CREATE PROCEDURE [dbo].[spDeleteProjectRoleGroup]
   @ProjectRoleGroupId int
AS
BEGIN
	DECLARE @temp INT;

	-- Check if this ProjectRoleGroup is being used by a Project
	SELECT @temp = COUNT(*) FROM [dbo].[Project] WHERE ProjectRoleGroupId = @ProjectRoleGroupId;
	IF (@temp > 0)
		RAISERROR('Cannot delete project role group while a project is using it', 16, 1);
	ELSE
	BEGIN
		
		-- Delete any project role links which are using this role group
		DELETE FROM [dbo].[ProjectRoleLink]
		WHERE ProjectRoleGroupId = @ProjectRoleGroupId;

		-- Delete project role group
		DELETE FROM [dbo].[ProjectRoleGroup]
		WHERE ProjectRoleGroupId = @ProjectRoleGroupId;
	END;
END;
