CREATE PROCEDURE [dbo].[spGetProjectOfferingsForUserId]
	@UserId int
AS
BEGIN

	-- Get list of projects
	SELECT *
	FROM [dbo].[ProjectOffering]
	WHERE UnitOfferingId IN (

		-- Get unit offering ID from user ID (via Enrollment, for students)
		SELECT UnitOfferingId
		FROM [dbo].[Enrollment]
		WHERE UserId = @UserId

		UNION
					
		-- Get unit offering ID from user ID (via UnitOffering, for conveners)
		SELECT UnitOfferingId
		FROM [dbo].[UnitOffering]
		WHERE ConvenorId = @UserId
	);
END;
