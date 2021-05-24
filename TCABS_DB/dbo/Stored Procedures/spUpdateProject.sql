CREATE PROCEDURE [dbo].[spUpdateProject]
   @ProjectId INT,
   @Name NVARCHAR(50),
   @Description NVARCHAR(MAX),
   @ProjectRoleGroupId INT
AS
BEGIN
   SET NOCOUNT ON;

	UPDATE [dbo].[Project]
	SET 
		[Name] = @Name,
		[Description] = @Description,
		[ProjectRoleGroupId] = @ProjectRoleGroupId
	WHERE [ProjectId] = @ProjectId;
END;
