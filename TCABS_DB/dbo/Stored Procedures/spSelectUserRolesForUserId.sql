CREATE PROCEDURE [dbo].[spSelectUserRolesForUserId]
   @userid int
AS
BEGIN
   SELECT * FROM [dbo].[UserRole]
   WHERE UserId = @userid
END;
