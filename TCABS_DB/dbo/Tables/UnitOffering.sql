CREATE TABLE [dbo].[UnitOffering]
(
	[UnitOfferingId]   INT IDENTITY(1,1) NOT NULL,
	[ConvenorId]       INT NOT NULL,
	[UnitId]           INT NOT NULL,
	[TeachingPeriodId] INT NOT NULL,
	[YearId]           INT NOT NULL,
	[ProjectId]			 INT NULL,
	CONSTRAINT PK_UnitOfferingId PRIMARY KEY ([UnitOfferingId]),
	CONSTRAINT FK_UnitOffering_ConvenorId       FOREIGN KEY (ConvenorId)       REFERENCES [User] (UserId),
	CONSTRAINT FK_UnitOffering_UnitId           FOREIGN KEY (UnitId)           REFERENCES [Unit] (UnitId),
	CONSTRAINT FK_UnitOffering_TeachingPeriodId FOREIGN KEY (TeachingPeriodId) REFERENCES [TeachingPeriod] (TeachingPeriodId),
	CONSTRAINT FK_UnitOffering_YearId           FOREIGN KEY (YearId)           REFERENCES [Year] (YearId),
	CONSTRAINT FK_UnitOffering_ProjectId		  FOREIGN KEY (ProjectId)			REFERENCES [Project] (ProjectId)
)
