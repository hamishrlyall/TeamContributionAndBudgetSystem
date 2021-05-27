CREATE PROCEDURE [dbo].[spGetAllProjectRoles]
AS
BEGIN
	SELECT * FROM [dbo].[ProjectRole] ORDER BY [Name] ASC;
END;
