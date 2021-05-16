CREATE PROCEDURE [dbo].[spSelectRoleForRoleId]
   @roleid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[Role]
   WHERE [RoleId] = @roleid
END;

