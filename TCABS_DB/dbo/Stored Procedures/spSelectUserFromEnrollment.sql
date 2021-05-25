CREATE PROCEDURE [dbo].[spSelectUserFromEnrollment]
	@EnrollmentId int
AS
BEGIN

	-- Get user data
	SELECT *
	FROM [dbo].[User]
	WHERE UserId = (

		-- Get user ID
		SELECT UserId
		FROM [dbo].[Enrollment]
		WHERE EnrollmentId = @EnrollmentId
	);
END;
