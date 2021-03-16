CREATE TABLE [dbo].[Permission]
(
   [PermissionId] INT IDENTITY(1,1) NOT NULL,
   [Name] NVARCHAR(50) NOT NULL,
   CONSTRAINT PK_PermissionId PRIMARY KEY (PermissionId)
)
