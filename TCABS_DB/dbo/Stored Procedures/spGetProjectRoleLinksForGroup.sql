CREATE PROCEDURE [dbo].[spGetProjectRoleLinksForGroup]
	@ProjectRoleGroupId int
AS
BEGIN
	SELECT *
	FROM [dbo].[ProjectRoleLink]
	WHERE ProjectRoleGroupId = @ProjectRoleGroupId
END;
