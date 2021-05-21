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

-- Create test users
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

-- Create roles
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

-- Create permissions
-- Update this when new stored procedures are added
-- [PermissionId], [PermissionName], [LinkTitle], [LinkPage], [LinkController]
MERGE INTO [Permission] AS Target USING (
   VALUES
   (1, 'UserModify', NULL, NULL, NULL),
   (2, 'UserRoleDelete', NULL, NULL, NULL),
   (3, 'UserView', 'List Users', 'Index', 'User'),
   (4, 'UserRoleModify', NULL, NULL, NULL),
   (5, 'RolePermissionView', 'Role Permissions', 'Index', 'RolePermission'),
   (6, 'ProjectView', 'Projects', 'Index', 'Project')
)
AS Source ([PermissionId], [PermissionName], [LinkTitle], [LinkPage], [LinkController])
ON Target.PermissionId = Source.PermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([PermissionName], [LinkTitle], [LinkPage], [LinkController])
VALUES ([PermissionName], [LinkTitle], [LinkPage], [LinkController]);


-- Link Users to roles
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

-- Link permissions to roles
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
   (11, 2, 5),
   (12, 2, 6)
)
AS Source ([RolePermissionId], [RoleId], [PermissionId])
ON Target.RolePermissionId = Source.RolePermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [RoleId], [PermissionId])
VALUES ( [RoleId], [PermissionId]);


-- Create project role groups
MERGE INTO [ProjectRoleGroup] AS Target USING (
   VALUES
   (1, 'RoleGroup1'),
   (2, 'RoleGroup2')
)
AS Source ([ProjectRoleGroupId], [Name])
ON Target.ProjectRoleGroupId = Source.ProjectRoleGroupId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name])
VALUES ([Name]);


-- Create projects
MERGE INTO [Project] AS Target USING (
   VALUES
   (1, 'Project1','Description for Project1', 1),
   (2, 'Project2','Description for Project2', 1)
)
AS Source ([ProjectId], [Name], [Description], [ProjectRoleGroupId])
ON Target.ProjectId = Source.ProjectRoleGroupId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name], [Description], [ProjectRoleGroupId])
VALUES ([Name], [Description], [ProjectRoleGroupId]);


END;