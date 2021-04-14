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
BEGIN

MERGE INTO [User] AS Target USING ( 
   VALUES
   (1, 'superadmin', 'super', 'user', 'superadmin@email.com', 0400000000, 'password'),
   (2, 'admin','admin', 'user', 'admin@email.com', 0400000000, 'password'),
   (3, 'convenor','convenor', 'user', 'convener@email.com', 0400000000, 'password'),
   (4, 'student','student', 'user', 'student@email.com', 0400000000, 'password'),
   (5, 'supervisor','supervisor', 'user', 'supervisor@email.com', 0400000000, 'password')
)
AS Source ([UserId], [UserName], [FirstName], [LastName], [Email], [PhoneNo], [Password]) 
ON Target.UserId = Source.UserId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([UserName], [FirstName], [LastName], [Email], [PhoneNo], [Password])
VALUES ([UserName], [FirstName], [LastName], [Email], [PhoneNo], [Password]);

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
   (1, 'User', 'Read'),
   (2, 'User', 'Create'),
   (3, 'User', 'Update'),
   (4, 'User', 'Delete'),
   (5, 'UserRole', 'Read'),
   (6, 'UserRole', 'Create'),
   (7, 'UserRole', 'Update'),
   (8, 'UserRole', 'Delete'),
   (9, 'RolePermission', 'Read'),
   (10, 'RolePermission', 'Create'),
   (11, 'RolePermission', 'Update'),
   (12, 'RolePermission', 'Delete')
)
AS Source ([PermissionId], [TableName], [Action])
ON Target.PermissionId = Source.PermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([TableName], [Action])
VALUES ([TableName], [Action]);

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
   (7, 1, 7),
   (8, 1, 8),
   (9, 1, 9),
   (10, 1, 10),
   (11, 1, 11),
   (12, 1, 12),
   (13, 2, 1),
   (14, 2, 2),
   (15, 2, 3),
   (16, 2, 4),
   (17, 2, 5),
   (18, 2, 6),
   (19, 2, 7),
   (20, 2, 8),
   (21, 2, 9),
   (22, 2, 10),
   (23, 2, 11),
   (24, 2, 12)
)
AS Source ([RolePermissionId], [RoleId], [PermissionId])
ON Target.RolePermissionId = Source.RolePermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [RoleId], [PermissionId])
VALUES ( [RoleId], [PermissionId]);

END;