CREATE PROCEDURE [dbo].[spGetUserRolesByUserId]
   @userid int = null
AS
BEGIN
   SET NOCOUNT ON;

   SELECT r.[RoleId], r.[Name]
   FROM [User] u
   LEFT OUTER JOIN [UserRole] ur ON ur.UserId = u.UserId
   LEFT OUTER JOIN [Role] r ON r.RoleId = ur.RoleId
   WHERE u.UserId = @userid
END;