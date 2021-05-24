CREATE TABLE [dbo].[TeachingPeriod]
(
	[TeachingPeriodId] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Day] INT NOT NULL,
	[Month] INT NOT NULL,
	CONSTRAINT PK_TeachingPeriodId PRIMARY KEY (TeachingPeriodId),
	CONSTRAINT UQ_TeachingPeriod_Name UNIQUE ([Name])
)
