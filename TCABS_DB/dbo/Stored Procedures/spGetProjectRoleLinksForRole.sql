CREATE PROCEDURE [dbo].[spGetProjectRoleLinksForRole]
	@ProjectRoleId int
AS
BEGIN
	SELECT *
	FROM [dbo].[ProjectRoleLink]
	WHERE ProjectRoleId = @ProjectRoleId
END;
