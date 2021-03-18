-- This Stored Procedure will check if another UserRole exists with the same UserId and RoleId.
-- If exists this Stored Procedure will return an error code.
-- If doesn't exist this stored procedure will insert the new UserRole with the provided UserId and RoleId.

CREATE PROCEDURE [dbo].[spInsertUserRole]
   @UserId int,
   @RoleId int,
   @UserRoleId int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[UserRole]([UserId], [RoleId])
   SELECT @UserId, @RoleId
   WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId );

   --IF @@rowcount = 0 RETURN 99;

   SET @UserRoleId = scope_identity();
   SELECT * FROM [dbo].[UserRole] WHERE [UserRoleId] = @UserRoleId;

   --  RETURN 0
END

