CREATE PROCEDURE [dbo].[spGetProjectRoleGroup]
	@ProjectRoleGroupId int
AS
BEGIN
	SELECT *
	FROM [dbo].[ProjectRoleGroup]
	WHERE ProjectRoleGroupId = @ProjectRoleGroupId
END;
