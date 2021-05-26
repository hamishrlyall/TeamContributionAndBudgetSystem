CREATE PROCEDURE [dbo].[spCreateProjectTask]
	@Description NVARCHAR(100),
	@EnrollmentId INT,
	@ProjectRoleId INT,
	@Duration INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[ProjectTask] ([Description], [EnrollmentId], [ProjectRoleId], [Duration], [Approved], [Modified])
	VALUES (@Description, @EnrollmentId, @ProjectRoleId, @Duration, 0, 0)
END;
