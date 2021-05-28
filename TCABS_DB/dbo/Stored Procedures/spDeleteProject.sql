CREATE PROCEDURE [dbo].[spDeleteProject]
   @ProjectId int
AS
BEGIN
	DECLARE @temp INT;

	-- Check if this Project is being used by a ProjectOffering
	SELECT @temp = COUNT(*) FROM [dbo].[ProjectOffering] WHERE ProjectId = @ProjectId;
	IF (@temp > 0)
		RAISERROR('Cannot delete project type while a project is using it', 16, 1);
	ELSE
	BEGIN
	
		-- Delete project
		DELETE FROM [dbo].[Project]
		WHERE ProjectId = @ProjectId;
   END;
END;
