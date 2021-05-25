CREATE PROCEDURE [dbo].[spDeleteProjectRole]
   @ProjectRoleId int
AS
BEGIN
   DELETE FROM [dbo].[ProjectRole]
   WHERE ProjectRoleId = @ProjectRoleId
END;
