CREATE PROCEDURE [dbo].[spSelectUserRoleCountForUserIdAndRoleId]
   @userid int,
   @roleid int
AS
BEGIN
   SELECT COUNT(*) FROM [UserRole]
   WHERE [UserId] = @userid AND [RoleId] = @roleid
END;
