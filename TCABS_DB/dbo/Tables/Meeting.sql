CREATE TABLE [dbo].[Meeting]
(
	[MeetingId] INT IDENTITY (1,1) NOT NULL,
	[ScheduledDateTime] DATETIME NOT NULL,
	[TeamId] INT NOT NULL,
	CONSTRAINT PK_MeetingId PRIMARY KEY (MeetingId),
	CONSTRAINT FK_Meeting_TeamId FOREIGN KEY (TeamId) REFERENCES [Team] (TeamId)
)
