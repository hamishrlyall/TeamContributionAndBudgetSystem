CREATE PROCEDURE [dbo].[spGetAllProjectTasks]
AS
BEGIN
	SELECT * FROM [dbo].[ProjectTask] ORDER BY [Description] ASC;
END;
