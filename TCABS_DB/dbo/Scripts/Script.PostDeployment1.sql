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
   (3, 'Convenor'),
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
   (3, 'UserView', 'Users', 'Index', 'User'),
   (4, 'UserRoleModify', NULL, NULL, NULL),
   (5, 'UnitManagement', 'Unit Management', 'Index', 'UnitOffering'),
   (6, 'RolePermission', 'Permissions', 'Index', 'RolePermission'),
   (7, 'Project', 'Projects', 'Index', 'ProjectOffering'),
   (8, 'ProjectRole', 'Project Roles', 'Index', 'ProjectRole'),
   (9, 'ProjectRoleGroup', 'Project Role Groups', 'Index', 'ProjectRoleGroup')
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
   (12, 2, 6),
   (13, 1, 7),
   (14, 2, 7),
   (15, 1, 8),
   (16, 2, 8),
   (17, 1, 9),
   (18, 2, 9)
)
AS Source ([RolePermissionId], [RoleId], [PermissionId])
ON Target.RolePermissionId = Source.RolePermissionId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [RoleId], [PermissionId])
VALUES ( [RoleId], [PermissionId]);

MERGE INTO [Unit] AS Target USING (
   VALUES
   (1, 'INF300111' ),
   (2, 'SWE30010' ),
   (3, 'COS20001' ),
   (4, 'SWD5002' )
)
AS Source ([UnitId], [Name])
ON Target.UnitId = Source.UnitId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [Name] )
VALUES ( [Name] );

MERGE INTO [TeachingPeriod] AS Target USING(
   VALUES
   ( 1, 'Summer', 1, 4 ),
   ( 2, 'Semester 1', 3, 1 ),
   ( 3, 'Winter', 6, 21 ),
   ( 4, 'Semester 2', 8, 2 )
)
AS Source ([TeachingPeriodId], [Name], [Month], [Day] )
ON Target.TeachingPeriodId = Source.TeachingPeriodId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [Name], [Month], [Day] )
VALUES ( [Name], [Month], [Day] );

MERGE INTO [Year] AS Target USING(
   VALUES
   ( 1, 2020 ),
   ( 2, 2021 ),
   ( 3, 2022 ),
   ( 4, 2023 ),
   ( 5, 2024 )
)
AS Source ([YearId], [Year])
ON Target.YearId = Source.YearId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [Year] )
VALUES ( [Year] );

MERGE INTO [UnitOffering] AS Target USING(
   VALUES
   ( 1, 3, 1, 2, 1 ),
   ( 2, 3, 2, 2, 1 ),
   ( 3, 3, 3, 2, 1 ),
   ( 4, 3, 4, 2, 1 )
)
AS Source ([UnitOfferingId], [ConvenorId], [UnitId], [TeachingPeriodId], [YearId] )
ON TARGET.UnitOfferingId = Source.UnitOfferingId
WHEN NOT MATCHED BY TARGET THEN
INSERT ( [ConvenorId], [UnitId], [TeachingPeriodId], [YearId] )
VALUES ( [ConvenorId], [UnitId], [TeachingPeriodId], [YearId] );

-- Create project roles
MERGE INTO [ProjectRole] AS Target USING (
   VALUES
   (1, 'Team Leader'),
   (2, 'Programmer')
)
AS Source ([ProjectRoleId], [Name])
ON Target.[ProjectRoleId] = Source.[ProjectRoleId]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name])
VALUES ([Name]);


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


-- Create project role linkss
MERGE INTO [ProjectRoleLink] AS Target USING (
   VALUES
   (1, 1, 1),
   (2, 2, 1)
)
AS Source ([ProjectRoleLinkId], [ProjectRoleId], [ProjectRoleGroupId])
ON Target.ProjectRoleLinkId = Source.ProjectRoleLinkId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ProjectRoleId], [ProjectRoleGroupId])
VALUES ([ProjectRoleId], [ProjectRoleGroupId]);



END;