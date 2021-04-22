-- This Stored Procedure will check if another UserRole exists with the same UserId and RoleId.
-- If exists this Stored Procedure will return an error code.
-- If doesn't exist this stored procedure will insert the new UserRole with the provided UserId and RoleId.

CREATE PROCEDURE [dbo].[spInsertUserRole]
   @userid int,
   @roleid int,
   @userroleid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[UserRole]([UserId], [RoleId])
   SELECT @userid, @roleid
   WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[UserRole] WHERE [UserId] = @userid AND [RoleId] = @roleid );

   --IF @@rowcount = 0 RETURN 99;

   SET @userroleid = scope_identity();
   SELECT * FROM [dbo].[UserRole] WHERE [UserRoleId] = @userroleid;

   --  RETURN 0
END;

