CREATE TABLE [dbo].[RolePermission]
(
   [RolePermissionId] INT IDENTITY(1,1) NOT NULL,
   [RoleId] INT NOT NULL,
   [PermissionId] INT NOT NULL,
   CONSTRAINT PK_RolePermissionId PRIMARY KEY ([RolePermissionId]),
   CONSTRAINT FK_RolePermission_RoleId FOREIGN KEY (RoleId) REFERENCES [Role] (RoleId),
   CONSTRAINT FK_RolePermission_PermissionId FOREIGN KEY (PermissionId) REFERENCES [Permission] (PermissionId)
)
