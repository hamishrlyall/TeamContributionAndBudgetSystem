-- This Stored Procedure will check if another UserRole exists with the same UserId and RoleId.
-- If exists this Stored Procedure not insert the userrole
-- The stored procedure will check for the type of Role being added
-- If the role is Student the Stored procedure will ensure the User has no other related user roles.
-- If the role is anything else the Stored procedure will ensure the User has no UserRole where the Role is Student.
-- If doesn't exist this stored procedure will insert the new UserRole with the provided UserId and RoleId.

CREATE PROCEDURE [dbo].[spInsertUserRole]
   @userid int,
   @roleid int,
   @userroleid int out
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @rolename NVARCHAR(50)
   SELECT @rolename = [Name]
   FROM [dbo].[Role]
   WHERE [RoleId] = @roleid;

   DECLARE @userrolecount INT
   SELECT @userrolecount = COUNT(*)
   FROM [dbo].[UserRole]
   WHERE [UserId] = @userid;

   DECLARE @studentroleid INT
   SELECT @studentroleid = [RoleId]
   FROM [dbo].[Role]
   WHERE [Name] = 'Student';

   DECLARE @studentrolecount INT
   SELECT @studentrolecount = COUNT(*)
   FROM [dbo].[UserRole]
   WHERE [UserId] = @userid AND [RoleId] = @studentroleid;

   IF( @userrolecount > 0 AND @rolename = 'Student' )
   BEGIN
      RETURN;
   END
   ELSE IF( @studentrolecount > 0 )
   BEGIN
      RETURN;
   END
   ELSE
   BEGIN
      INSERT INTO [dbo].[UserRole]([UserId], [RoleId])
      SELECT @userid, @roleid
      WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[UserRole] 
      WHERE [UserId] = @userid AND [RoleId] = @roleid );

      SET @userroleid = scope_identity();
      SELECT * FROM [dbo].[UserRole] WHERE [UserRoleId] = @userroleid;
   END
END;