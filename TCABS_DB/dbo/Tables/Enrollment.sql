CREATE TABLE [dbo].[Enrollment]
(
	[EnrollmentId]   INT IDENTITY(1,1) NOT NULL,
	[UserId]         INT NOT NULL,
	[UnitOfferingId] INT NOT NULL,
	[TeamId]         INT,
	CONSTRAINT PK_EnrollmentId PRIMARY KEY (EnrollmentId),
	CONSTRAINT FK_Enrollment_UserId         FOREIGN KEY (UserId)         REFERENCES [User] (UserId),
	CONSTRAINT FK_Enrollment_UnitOfferingId FOREIGN KEY (UnitOfferingId) REFERENCES [UnitOffering] (UnitOfferingId),
	CONSTRAINT FK_Enrollment_TeamId         FOREIGN KEY (TeamId)         REFERENCES [Team] (TeamId)
)
