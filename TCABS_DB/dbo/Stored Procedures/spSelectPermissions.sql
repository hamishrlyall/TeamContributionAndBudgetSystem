CREATE PROCEDURE [dbo].[spSelectPermissions]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[Permission] ORDER BY [PermissionName] ASC;
END;



