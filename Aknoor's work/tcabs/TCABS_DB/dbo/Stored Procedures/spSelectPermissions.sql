CREATE PROCEDURE [dbo].[spSelectPermissions]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT (TableName + ','+ Action) as DisplayValue, PermissionId FROM [dbo].[Permission]
END;



