CREATE TABLE [dbo].[ProjectRoleGroup]
(
	[ProjectRoleGroupId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_ProjectRoleGroupId PRIMARY KEY (ProjectRoleGroupId),
	CONSTRAINT UQ_ProjectRoleGroup_Name UNIQUE (Name)
)
