CREATE PROCEDURE [dbo].[spDeleteProjectRoleGroup]
   @ProjectRoleGroupId int
AS
BEGIN
   DELETE FROM [dbo].[ProjectRoleGroup]
   WHERE ProjectRoleGroupId = @ProjectRoleGroupId
END;
