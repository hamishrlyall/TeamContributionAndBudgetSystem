CREATE PROCEDURE [dbo].[spCreateProjectRoleGroup]
	@Name NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[ProjectRoleGroup] ([Name])
	VALUES (@Name)
END;
