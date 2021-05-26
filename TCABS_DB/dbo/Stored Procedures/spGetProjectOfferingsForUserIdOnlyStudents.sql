CREATE PROCEDURE [dbo].[spGetProjectOfferingsForUserIdOnlyStudents]
	@UserId int
AS
BEGIN

	-- Get list of project offerings
	SELECT *
	FROM [dbo].[ProjectOffering]
	WHERE UnitOfferingId IN (

		-- Get unit offering ID from user ID (via Enrollment, for students)
		SELECT UnitOfferingId
		FROM [dbo].[Enrollment]
		WHERE UserId = @UserId
	);
END;
