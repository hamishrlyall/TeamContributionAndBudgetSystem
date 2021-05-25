CREATE PROCEDURE [dbo].[spUpdateProjectRoleGroup]
   @ProjectRoleGroupId INT,
   @Name NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;

	UPDATE [dbo].[ProjectRoleGroup]
	SET [Name] = @Name
	WHERE [ProjectRoleGroupId] = @ProjectRoleGroupId;
END;
