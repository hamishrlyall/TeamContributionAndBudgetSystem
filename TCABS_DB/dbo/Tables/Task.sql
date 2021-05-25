CREATE TABLE [dbo].[Task]
(
	[TaskId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[ProjectRoleId] INT NOT NULL,
	CONSTRAINT PK_TaskId PRIMARY KEY (TaskId),
	CONSTRAINT FK_Task_ProjectRoleId FOREIGN KEY (ProjectRoleId) REFERENCES [ProjectRole] (ProjectRoleId)
)
