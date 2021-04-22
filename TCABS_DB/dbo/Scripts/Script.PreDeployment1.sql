/*
 Pre-Deployment Script Template                     
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.   
 Use SQLCMD syntax to include a file in the pre-deployment script.         
 Example:      :r .\myfile.sql                        
 Use SQLCMD syntax to reference a variable in the pre-deployment script.      
 Example:      :setvar TableName MyTable                     
               SELECT * FROM [$(TableName)]               
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS [dbo].[RolePermission];

DROP TABLE IF EXISTS [dbo].[Permission];

DROP TABLE IF EXISTS [dbo].[UserRole];

DROP TABLE IF EXISTS [dbo].[Role];

DROP TABLE IF EXISTS [dbo].[User];

DROP PROCEDURE IF EXISTS [dbo].[spCreateUser];

DROP PROCEDURE IF EXISTS [dbo].[spGetUserRoleByUserId];

DROP PROCEDURE IF EXISTS [dbo].[spInsertUserRole];

DROP PROCEDURE IF EXISTS [dbo].[spLoadUsers];

DROP PROCEDURE IF EXISTS [dbo].[spSelectUserForUserId];

DROP PROCEDURE IF EXISTS [dbo].[spSelectUserWithRoles];

DROP PROCEDURE IF EXISTS [dbo].[spLoadUsers];

DROP PROCEDURE IF EXISTS [dbo].[spUpdatePassword]