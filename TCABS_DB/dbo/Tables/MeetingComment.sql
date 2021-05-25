CREATE TABLE [dbo].[MeetingComment]
(
	[MeetingCommentId] INT IDENTITY (1,1) NOT NULL,
	[MeetingId] INT NOT NULL,
	[Comment]  NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_MeetingCommentId PRIMARY KEY (MeetingCommentId),
	CONSTRAINT FK_MeetingComment_MeetingId FOREIGN KEY (MeetingId) REFERENCES [Meeting] (MeetingId)
)
