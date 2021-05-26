CREATE TABLE [dbo].[Team]
(
	[TeamId]       INT IDENTITY(1,1) NOT NULL,
	[SupervisorId] INT NOT NULL,
	[ProjectOfferingId]    INT NOT NULL,
	[Name]         NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_TeamId						  PRIMARY KEY (TeamId),
	CONSTRAINT FK_Team_SupervisorId	     FOREIGN KEY (SupervisorId)			 REFERENCES [User] (UserId),
	CONSTRAINT FK_Team_ProjectOfferingId  FOREIGN KEY (ProjectOfferingId)    REFERENCES [ProjectOffering] (ProjectOfferingId)
)
