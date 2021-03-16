/*
Post-Deployment Script Template                     
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.      
 Use SQLCMD syntax to include a file in the post-deployment script.         
 Example:      :r .\myfile.sql                        
 Use SQLCMD syntax to reference a variable in the post-deployment script.      
 Example:      :setvar TableName MyTable                     
               SELECT * FROM [$(TableName)]               
--------------------------------------------------------------------------------------
*/
--IF '$(LoadTestData)' = 'true'

BEGIN

MERGE INTO [User] AS Target USING ( 
   VALUES
   (1, 'superadmin', 'superadmin@email.com', 0400000000, 'password'),
   (2, 'admin', 'admin@email.com', 0400000000, 'password'),
   (3, 'convener', 'convener@email.com', 0400000000, 'password'),
   (4, 'student', 'student@email.com', 0400000000, 'password'),
   (5, 'supervisor', 'supervisor@email.com', 0400000000, 'password')
)
AS Source ([UserId], [UserName], [Email], [PhoneNo], [Password]) 
ON Target.UserId = Source.UserId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserName], [Email], [PhoneNo], [Password])
VALUES ([UserName], [Email], [PhoneNo], [Password]);

MERGE INTO [Role] AS Target USING (
   VALUES
   (1, 'Super Admin'),
   (2, 'Admin'),
   (3, 'Convener'),
   (4, 'Student'),
   (5, 'Supervisor')
)
AS Source ([RoleId], [Name])
ON Target.RoleId = Source.RoleId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name])
VALUES ([Name]);

MERGE INTO [Permission] AS Target USING (
   VALUES
   (1, 'Create Employee'),
   (2, 'Create Unit of Study'),
   (3, 'Register Student'),
   (4, 'Enrol Student in Unit'),
   (5, 'Register Teams'),
   (6, 'Register Projects')
)
AS Source ([PermissionId], [Name])
ON Target.PermissionId = Source.PermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name])
VALUES ([Name]);

MERGE INTO [UserRole] AS Target USING (
 VALUES
 (1, 1, 1),
 (2, 2, 2),
 (3, 3, 3),
 (4, 4, 4),
 (5, 5, 5)
 )
 AS Source ([UserRoleId], [UserId], [RoleId])
 ON Target.UserRoleId = Source.UserRoleId
 WHEN NOT MATCHED BY TARGET THEN
 INSERT ([UserId], [RoleId])
 VALUES ([UserId], [RoleId]);

MERGE INTO [RolePermission] AS Target USING (
   VALUES
   (1, 1, 1),
   (2, 1, 2),
   (3, 1, 3),
   (4, 1, 4),
   (5, 1, 5),
   (6, 1, 6),
   (7, 2, 1),
   (8, 2, 2),
   (9, 2, 3),
   (10, 2, 4),
   (11, 3, 5),
   (12, 3, 6)
)
AS Source ([RolePermissionId], [RoleId], [PermissionId])
ON Target.RolePermissionId = Source.RolePermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [RoleId], [PermissionId])
VALUES ( [RoleId], [PermissionId]);

END;