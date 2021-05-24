CREATE PROCEDURE [dbo].[spCreateProjectRoleLink]
	@ProjectRoleId INT,
	@ProjectRoleGroupId INT
AS
BEGIN
	SET NOCOUNT ON;

	
END;

BEGIN
	DECLARE @temp INT;

	-- Check if a link with the same data already exists
	SELECT @temp = COUNT(*) FROM [dbo].[ProjectRoleLink] WHERE ProjectRoleGroupId = @ProjectRoleGroupId AND ProjectRoleId = @ProjectRoleId;
	IF (@temp > 0)
		RAISERROR('Already assigned', 16, 1);
	ELSE
	BEGIN
		
		-- Create project role link
		INSERT INTO [dbo].[ProjectRoleLink] ([ProjectRoleId], [ProjectRoleGroupId])
		VALUES (@ProjectRoleId, @ProjectRoleGroupId)
	END;
END;

