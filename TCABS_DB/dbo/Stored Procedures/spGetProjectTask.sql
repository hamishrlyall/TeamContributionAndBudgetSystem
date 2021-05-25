CREATE PROCEDURE [dbo].[spGetProjectTask]
	@TaskId int
AS
BEGIN
	SELECT *
	FROM [dbo].[ProjectTask]
	WHERE [ProjectTaskId] = @TaskId
END;
