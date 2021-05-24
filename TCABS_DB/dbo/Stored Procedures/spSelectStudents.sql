CREATE PROCEDURE [dbo].[spSelectStudents]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [User].[UserId], COUNT(*)
   FROM [dbo].[User]
   INNER JOIN [dbo].[UserRole]
      ON [UserRole].[UserId] = [User].[UserId]
   INNER JOIN [dbo].[Role]
      ON [UserRole].[RoleId] = [Role].[RoleId]
   WHERE [Role].[Name] = 'Student'
   GROUP BY [User].[UserId]
   HAVING COUNT(*) = (
      SELECT COUNT([Role].[RoleId])
      FROM [Role] WHERE [Role].[Name] = 'Student'
   )
END;
