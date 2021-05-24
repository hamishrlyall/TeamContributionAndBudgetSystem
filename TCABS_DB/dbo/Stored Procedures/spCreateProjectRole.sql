CREATE PROCEDURE [dbo].[spCreateProjectRole]
	@Name NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[ProjectRole] ([Name])
	VALUES (@Name)
END;
