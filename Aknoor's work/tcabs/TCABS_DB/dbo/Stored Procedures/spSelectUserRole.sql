CREATE PROCEDURE [dbo].[spSelectUserRole]
   @userroleid int
AS
BEGIN
   SELECT * FROM [dbo].[UserRole]
   WHERE UserRoleId = @userroleid
END;
