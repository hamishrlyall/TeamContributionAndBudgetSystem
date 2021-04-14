CREATE PROCEDURE [dbo].[spDeleteUserRole]
   @userroleid int
AS
BEGIN
   DELETE [UserRole] 
   WHERE UserRoleId = @userroleid
END;
