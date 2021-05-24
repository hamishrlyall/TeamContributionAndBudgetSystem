CREATE TABLE [dbo].[ProjectRoleLink]
(
	[ProjectRoleLinkId] INT IDENTITY(1,1) NOT NULL,
	[ProjectRoleGroupId] INT NOT NULL,
	[ProjectRoleId] INT NOT NULL,
	CONSTRAINT PK_ProjectRoleLinkId PRIMARY KEY (ProjectRoleLinkId),
	CONSTRAINT FK_ProjectRoleLink_ProjectRoleGroupId FOREIGN KEY (ProjectRoleGroupId) REFERENCES [ProjectRoleGroup] (ProjectRoleGroupId),
	CONSTRAINT FK_ProjectRoleLink_ProjectRoleId FOREIGN KEY (ProjectRoleId) REFERENCES [ProjectRole] (ProjectRoleId)
)
