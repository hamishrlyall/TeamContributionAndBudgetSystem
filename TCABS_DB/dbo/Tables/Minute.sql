CREATE TABLE [dbo].[Minute]
(
	[MinuteId] INT IDENTITY(1,1) NOT NULL,
	[Description] NVARCHAR(50) NOT NULL,
	[Duration] VARCHAR(20),
	[MeetingId] INT NOT NULL,
	[EnrollmentId] INT NOT NULL,
	[IsApproved] BIT,
	CONSTRAINT PK_MinuteId PRIMARY KEY (MinuteId),
	CONSTRAINT FK_Minute_MeetingId FOREIGN KEY (MeetingId) REFERENCES [Meeting] (MeetingId),
	CONSTRAINT FK_Minute_EnrollementId FOREIGN KEY (EnrollmentId) REFERENCES [Enrollment] (EnrollmentId)
)
