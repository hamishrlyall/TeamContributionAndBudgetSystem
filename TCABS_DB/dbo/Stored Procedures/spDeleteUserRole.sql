CREATE PROCEDURE [dbo].[spDeleteUserRole]
   @userroleid int
AS
BEGIN
   DECLARE @temproleid INT;
   SELECT @temproleid = [RoleId]
   FROM [dbo].[UserRole]
   WHERE UserRoleId = @userroleid;

   DECLARE @temprolename NVARCHAR(50)
   SELECT @temprolename = [Name]
   FROM [dbo].[Role]
   WHERE [RoleId] = @temproleid;

   IF( @temprolename != 'Super Admin' )
      DELETE [UserRole] 
      WHERE UserRoleId = @userroleid
END;
