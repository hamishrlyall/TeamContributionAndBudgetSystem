CREATE PROCEDURE [dbo].[spUpdateProjectRole]
   @ProjectRoleId INT,
   @Name NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;

	UPDATE [dbo].[ProjectRole]
	SET [Name] = @Name
	WHERE [ProjectRoleId] = @ProjectRoleId;
END;
