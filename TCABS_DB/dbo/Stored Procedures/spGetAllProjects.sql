CREATE PROCEDURE [dbo].[spGetAllProjects]
AS
BEGIN
	SELECT * FROM [dbo].[Project] ORDER BY [Name] ASC;
END;
