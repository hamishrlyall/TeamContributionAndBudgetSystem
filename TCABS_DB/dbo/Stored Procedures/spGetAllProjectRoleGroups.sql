CREATE PROCEDURE [dbo].[spGetAllProjectRoleGroups]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[ProjectRoleGroup];
END;
