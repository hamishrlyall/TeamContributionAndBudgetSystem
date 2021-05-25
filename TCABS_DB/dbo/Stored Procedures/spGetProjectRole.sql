CREATE PROCEDURE [dbo].[spGetProjectRole]
	@ProjectRoleId int
AS
BEGIN
	SELECT *
	FROM [dbo].[ProjectRole]
	WHERE ProjectRoleId = @ProjectRoleId
END;
