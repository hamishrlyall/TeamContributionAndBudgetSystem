CREATE TABLE [dbo].[UserRole]
(
   [UserRoleId] INT IDENTITY(1,1) NOT NULL,
   [UserId] INT NOT NULL,
   [RoleId] INT NOT NULL,
   CONSTRAINT PK_UserRoleId PRIMARY KEY ([UserRoleId]),
   CONSTRAINT FK_UserRole_UserId FOREIGN KEY (UserId) REFERENCES [User] (UserId),
   CONSTRAINT FK_UserRole_RoleId FOREIGN KEY (RoleId) REFERENCES [Role] (RoleId)
)
