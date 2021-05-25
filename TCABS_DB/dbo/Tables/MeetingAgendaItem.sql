CREATE TABLE [dbo].[MeetingAgendaItem]
(
	[MeetingAgendaItemId] INT IDENTITY(1,1) NOT NULL,
	[MeetingId] INT NOT NULL,
	[EnrollmentId] INT NOT NULL,
	[Comment]  NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_MeetingAgendaItemId PRIMARY KEY (MeetingAgendaItemId),
	CONSTRAINT FK_MeetingAgendaItem_MeetingId FOREIGN KEY (MeetingId) REFERENCES [Meeting] (MeetingId),
	CONSTRAINT FK_MeetingAgendaItem_EnrollementId FOREIGN KEY (EnrollmentId) REFERENCES [Enrollment] (EnrollmentId)
)
