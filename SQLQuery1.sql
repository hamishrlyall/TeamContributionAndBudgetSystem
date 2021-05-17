--CREATE TABLE [dbo].[User]
--(
--   [UserId] INT IDENTITY(1,1) NOT NULL,
--   [Username] NVARCHAR(50) NOT NULL,
--   [FirstName] NVARCHAR(100) NOT NULL,
--   [LastName] NVARCHAR(100) NOT NULL,
--   [Email] NVARCHAR(100) NOT NULL,
--   [PhoneNo] INT NOT NULL,
--   [Password] NVARCHAR(50) NOT NULL,
--   CONSTRAINT PK_UserId PRIMARY KEY (UserId),
--   CONSTRAINT Login_User_Username UNIQUE (Username)
--);
--CREATE TABLE [dbo].[Role]
--(
--   [RoleId] INT IDENTITY (1,1) NOT NULL,
--   [Name] NVARCHAR(50) NOT NULL,
--   CONSTRAINT PK_RoleId PRIMARY KEY (RoleId)
--);
--CREATE TABLE [dbo].[Permission]
--(
--   [PermissionId] INT IDENTITY(1,1) NOT NULL,
--   [TableName] NVARCHAR(50) NOT NULL,
--   [Action] NVARCHAR(50) NOT NULL,
--   CONSTRAINT PK_PermissionId PRIMARY KEY (PermissionId)
--);
--CREATE TABLE [dbo].[UserRole]
--(
--   [UserRoleId] INT IDENTITY(1,1) NOT NULL,
--   [UserId] INT NOT NULL,
--   [RoleId] INT NOT NULL,
--   CONSTRAINT PK_UserRoleId PRIMARY KEY ([UserRoleId]),
--   CONSTRAINT FK_UserRole_UserId FOREIGN KEY (UserId) REFERENCES [User] (UserId),
--   CONSTRAINT FK_UserRole_RoleId FOREIGN KEY (RoleId) REFERENCES [Role] (RoleId)
--);
--CREATE TABLE [dbo].[RolePermission]
--(
--   [RolePermissionId] INT IDENTITY(1,1) NOT NULL,
--   [RoleId] INT NOT NULL,
--   [PermissionId] INT NOT NULL,
--   CONSTRAINT PK_RolePermissionId PRIMARY KEY ([RolePermissionId]),
--   CONSTRAINT FK_RolePermission_RoleId FOREIGN KEY (RoleId) REFERENCES [Role] (RoleId),
--   CONSTRAINT FK_RolePermission_PermissionId FOREIGN KEY (PermissionId) REFERENCES [Permission] (PermissionId)
--);

CREATE PROCEDURE [dbo].[spCreateUser]
   @Username NVARCHAR(50),
   @FirstName NVARCHAR(50),
   @LastName NVARCHAR(100),
   @Email NVARCHAR(100),
   @PhoneNo INT,
   @Password NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[User] (Username, FirstName, LastName, Email, PhoneNo, [Password] )
   VALUES ( @Username, @FirstName, @LastName, @Email, @PhoneNo, @Password )
END;
go
CREATE PROCEDURE [dbo].[spDeleteUserRole]
   @userroleid int
AS
BEGIN
   DELETE [UserRole] 
   WHERE UserRoleId = @userroleid
END;
GO
CREATE PROCEDURE [dbo].[spInsertUserRole]
   @userid int,
   @roleid int,
   @userroleid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[UserRole]([UserId], [RoleId])
   SELECT @UserId, @RoleId
   WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId );

   --IF @@rowcount = 0 RETURN 99;

   SET @userroleid = scope_identity();
   SELECT * FROM [dbo].[UserRole] WHERE [UserRoleId] = @userroleid;

   --  RETURN 0
END;
GO
CREATE PROCEDURE [dbo].[spSelectPermission]
   @permissionid int
AS
BEGIN
   SELECT * FROM [dbo].[Permission]
   WHERE PermissionId = @permissionid
END;
GO
CREATE PROCEDURE [dbo].[spSelectPermissions]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT (TableName + ','+ Action) as DisplayValue, PermissionId FROM [dbo].[Permission]
END;
GO
CREATE PROCEDURE [dbo].[spSelectRoleForRoleId]
   @roleid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[Role]
   WHERE [RoleId] = @roleid
END;
GO
CREATE PROCEDURE [dbo].[spSelectRolePermissionsForRoleId]
   @roleid int
AS
BEGIN
   SELECT r.[PermissionId]
   FROM [ Role ] r
   LEFT OUTER JOIN[ RolePermission ] rp ON rp.RoleId = r.RoleId
   LEFT OUTER JOIN[ Permission ] p ON p.PermissionId = rp.PermissionId
   WHERE r.RoleId = @roleid
END;
GO
CREATE PROCEDURE [dbo].[spSelectRoles]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[Role]
END;
GO
CREATE PROCEDURE [dbo].[spSelectRoleWithPermissions]
   @roleid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [dbo].[Role] 
   WHERE RoleId = @roleid;
   SELECT * FROM [dbo].[RolePermission] 
   WHERE [RoleId] = @roleid;
END;
GO
CREATE PROCEDURE [dbo].[spSelectUserForUserId]
   @userid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [Firstname], [Lastname], [Email], [PhoneNo], [Password]
   FROM [User]
   WHERE [UserId] = @userid
END;
GO
CREATE PROCEDURE [dbo].[spSelectUserForUsername]
   @Username NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [Firstname], [Lastname], [Email], [PhoneNo], [Password]
   FROM [User]
   WHERE [Username] = @Username
END;
GO
CREATE PROCEDURE [dbo].[spSelectUserRole]
   @userroleid int
AS
BEGIN
   SELECT * FROM [dbo].[UserRole]
   WHERE UserRoleId = @userroleid
END;
GO
CREATE PROCEDURE [dbo].[spSelectUserRoles]
   @param1 int = 0,
   @param2 int
AS
   SELECT @param1, @param2
RETURN 0
GO
CREATE PROCEDURE [dbo].[spSelectUserRolesForUserId]
   @userid int
AS
BEGIN
   SELECT * FROm [dbo].[UserRole]
   WHERE UserId = @userid
END;
GO
CREATE PROCEDURE [dbo].[spSelectUsers]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT UserId, Username, FirstName, LastName, Email, PhoneNo, [Password] 
   FROM [dbo].[User]
END;
GO
CREATE PROCEDURE [dbo].[spSelectUserWithRoles]
   @UserId int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [User]
   WHERE [UserId] = @UserId;
   SELECT * FROM [UserRole]
   WHERE [UserId] = @UserId;
END;


