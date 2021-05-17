CREATE PROCEDURE [dbo].[spSelectUserWithRoles]
   @UserId int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [User]
   WHERE [UserId] = @UserId;
   SELECT * FROM [UserRole]
   WHERE [UserId] = @UserId;
END;
