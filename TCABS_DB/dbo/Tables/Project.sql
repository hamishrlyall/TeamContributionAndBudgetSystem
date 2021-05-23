CREATE TABLE [dbo].[Project]
(
	[ProjectId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX),
	[ProjectRoleGroupId] INT,
	CONSTRAINT PK_ProjectId PRIMARY KEY (ProjectId),
	CONSTRAINT FK_Project_ProjectRoleGroupId FOREIGN KEY (ProjectRoleGroupId) REFERENCES [ProjectRoleGroup] (ProjectRoleGroupId),
	CONSTRAINT UQ_Project_Name UNIQUE (Name)
)
