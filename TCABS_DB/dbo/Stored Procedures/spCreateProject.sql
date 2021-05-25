CREATE PROCEDURE [dbo].[spCreateProject]
	@Name NVARCHAR(50),
	@Description NVARCHAR(MAX),
	@ProjectRoleGroupId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Project] ([Name], [Description], ProjectRoleGroupId)
	VALUES (@Name, @Description, @ProjectRoleGroupId)
END;
