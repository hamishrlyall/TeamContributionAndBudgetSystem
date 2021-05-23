CREATE TABLE [dbo].[ProjectRole]
(
	[ProjectRoleId]  INT IDENTITY(1,1) NOT NULL,
	[ProjectRoleGroupId] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_ProjectRoleId PRIMARY KEY (ProjectRoleId),
	CONSTRAINT FK_ProjectRole_ProjectRoleGroupId FOREIGN KEY (ProjectRoleGroupId) REFERENCES [ProjectRoleGroup] (ProjectRoleGroupId)
)
