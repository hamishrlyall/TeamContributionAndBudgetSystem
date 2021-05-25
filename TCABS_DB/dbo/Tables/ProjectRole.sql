CREATE TABLE [dbo].[ProjectRole]
(
	[ProjectRoleId]  INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_ProjectRoleId PRIMARY KEY (ProjectRoleId)
)
