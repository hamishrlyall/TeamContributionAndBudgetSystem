CREATE PROCEDURE [dbo].[spApproveProjectTask]
	@TaskId INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[ProjectTask]
	SET [Approved] = 1
	WHERE [ProjectTaskId] = @TaskId;
END;
