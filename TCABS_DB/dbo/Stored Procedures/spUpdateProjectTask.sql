CREATE PROCEDURE [dbo].[spUpdateProjectTask]
	@TaskId INT,
	@Description NVARCHAR(100),
	@ProjectRoleId INT,
	@Duration INT

AS
BEGIN

	-- Check if the task is approved yet
	DECLARE @approved BIT;
	SELECT @approved = [Approved] FROM [dbo].[ProjectTask] WHERE [ProjectTaskId] = @TaskId;

	-- Update task
	-- If the task is approved then it becomes marked as modified
	-- If it is not yet approved then it cannot be marked as modified
	UPDATE [dbo].[ProjectTask]
	SET [Description] = @Description,
		[ProjectRoleId] = @ProjectRoleId,
		[Duration] = @Duration
	WHERE [ProjectTaskId] = @TaskId;
END;
