CREATE TABLE [dbo].[Project]
(
	[ProjectId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(256),
	CONSTRAINT PK_ProjectId PRIMARY KEY (ProjectId)
)
