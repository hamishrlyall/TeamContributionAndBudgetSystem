CREATE TABLE [dbo].[ProjectTask]
(
	[ProjectTaskId] INT IDENTITY(1,1) NOT NULL,
	[Description] NVARCHAR(100) NOT NULL,
	[EnrollmentId] INT NOT NULL,
	[ProjectRoleId] INT NOT NULL,
	[Duration] INT NOT NULL,
	[Approved] BIT NOT NULL,
	[Modified] BIT NOT NULL,
	CONSTRAINT PK_ProjectTaskId PRIMARY KEY ([ProjectTaskId]),
	CONSTRAINT FK_ProjectTask_EnrollmentId FOREIGN KEY ([EnrollmentId]) REFERENCES [Enrollment] (EnrollmentId),
	CONSTRAINT FK_ProjectTask_ProjectRoleId FOREIGN KEY (ProjectRoleId) REFERENCES [ProjectRole] (ProjectRoleId)
)
