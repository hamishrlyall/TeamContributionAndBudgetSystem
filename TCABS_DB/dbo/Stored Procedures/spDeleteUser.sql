CREATE PROCEDURE [dbo].[spDeleteUser]
   @userid int
AS
BEGIN

	DECLARE @enrollmentCount INT;
	DECLARE @UnitofferingCount INT;
	DECLARE @TeamCount INT;

	SELECT @enrollmentCount = COUNT(*) FROM [dbo].[Enrollment] WHERE UserId = @userid;
	SELECT @unitofferingCount = COUNT(*) FROM [dbo].[UnitOffering] WHERE UserId = @userid;
	SELECT @TeamCount = COUNT(*) FROM [dbo].[Team] WHERE SupervisorId = @userid;

	IF @enrollmentCount = 0 AND @unitofferingCount = 0
	BEGIN

		DELETE FROM [dbo].[UserRole]
	   WHERE UserId = @userid;

	   DELETE FROM [dbo].[User] 
	   WHERE UserId = @userid;
	END
	ELSE
	BEGIN
		RAISERROR('Unable to delete user', 16, 1);
	END

END;
