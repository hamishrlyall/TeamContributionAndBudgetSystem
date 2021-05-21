CREATE PROCEDURE [dbo].[spDeleteUser]
   @userid int
AS
BEGIN
   DELETE [User] 
   WHERE UserId = @userid
END;
