CREATE PROCEDURE [dbo].[spGetProjectsForUserIdOnlyStudents]
	@UserId int
AS
BEGIN

	-- Get list of projects
	SELECT *
	FROM [dbo].[Project]
	WHERE ProjectId IN (

		-- Get unit offering ID from user ID (via Enrollment, for students)
		SELECT UnitOfferingId
		FROM [dbo].[Enrollment]
		WHERE UserId = @UserId
	);
END;
