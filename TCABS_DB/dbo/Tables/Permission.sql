CREATE TABLE [dbo].[Permission]
(
   [PermissionId] INT IDENTITY(1,1) NOT NULL,
   [PermissionName] NVARCHAR(50) NOT NULL,
   [LinkGroup] NVARCHAR(50),
   [LinkTitle] NVARCHAR(50),
   [LinkPage] NVARCHAR(50),
   [LinkController] NVARCHAR(50),
   CONSTRAINT PK_PermissionId PRIMARY KEY (PermissionId),
   CONSTRAINT UQ_Permission_PermissionName UNIQUE ([PermissionName])
)
