CREATE PROCEDURE [dbo].[spGetProjectRolesForEnrollment]
	@EnrollmentId int
AS
BEGIN

	-- Get list of project role data
	SELECT *
	FROM [dbo].[ProjectRole]
	WHERE ProjectRoleId IN (

		-- Get list of project role IDs
		SELECT ProjectRoleId
		FROM [dbo].[ProjectRoleLink]
		WHERE ProjectRoleGroupId = (

			-- Get project role group ID
			SELECT ProjectRoleGroupId
			FROM [dbo].[Project]
			WHERE ProjectId = (

				-- Get project ID
				SELECT ProjectId
				FROM [dbo].[UnitOffering]
				WHERE UnitOfferingId = (

					-- Get unit offering ID
					SELECT UnitOfferingId
					FROM [dbo].[Enrollment]
					WHERE EnrollmentId = @EnrollmentId
				)
			)
		)
	);
END;
