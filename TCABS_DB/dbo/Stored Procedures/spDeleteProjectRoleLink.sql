CREATE PROCEDURE [dbo].[spDeleteProjectRoleLink]
   @ProjectRoleLinkId int
AS
BEGIN
   DELETE FROM [dbo].[ProjectRoleLink]
   WHERE ProjectRoleLinkId = @ProjectRoleLinkId
END;
