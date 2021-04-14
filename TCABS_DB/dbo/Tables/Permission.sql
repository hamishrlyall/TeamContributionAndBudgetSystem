CREATE TABLE [dbo].[Permission]
(
   [PermissionId] INT IDENTITY(1,1) NOT NULL,
   [TableName] NVARCHAR(50) NOT NULL,
   [Action] NVARCHAR(50) NOT NULL,
   CONSTRAINT PK_PermissionId PRIMARY KEY (PermissionId)
)
