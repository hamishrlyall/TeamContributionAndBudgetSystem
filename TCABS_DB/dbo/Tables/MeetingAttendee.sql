CREATE TABLE [dbo].[MeetingAttendee]
(
	[MeetingAttendeeId] INT IDENTITY (1,1) NOT NULL,
	[MeetingId] INT NOT NULL,
	[EnrollmentId] INT NOT NULL,
	CONSTRAINT PK_MeetingAttendeeId PRIMARY KEY (MeetingAttendeeId),
	CONSTRAINT FK_MeetingAttendee_MeetingId FOREIGN KEY (MeetingId) REFERENCES [Meeting] (MeetingId),
	CONSTRAINT FK_MeetingAttendee_EnrollementId FOREIGN KEY (EnrollmentId) REFERENCES [Enrollment] (EnrollmentId)
)