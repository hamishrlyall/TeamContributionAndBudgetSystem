CREATE TABLE [dbo].[TeachingPeriod]
(
	[TeachingPeriodId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_TeachingPeriodId PRIMARY KEY (TeachingPeriodId)
)
