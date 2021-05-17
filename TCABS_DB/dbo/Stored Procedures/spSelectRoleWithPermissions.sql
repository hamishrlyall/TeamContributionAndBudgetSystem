CREATE PROCEDURE [dbo].[spSelectRoleWithPermissions]
   @roleid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[Role] 
   WHERE RoleId = @roleid;
   SELECT * FROM [dbo].[RolePermission] 
   WHERE [RoleId] = @roleid;
END;
