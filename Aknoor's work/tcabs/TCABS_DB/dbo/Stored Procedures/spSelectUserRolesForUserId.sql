CREATE PROCEDURE [dbo].[spSelectUserRolesForUserId]
   @userid int
AS
BEGIN
   SELECT * FROm [dbo].[UserRole]
   WHERE UserId = @userid
END;
