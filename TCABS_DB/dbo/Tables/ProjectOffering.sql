CREATE TABLE [dbo].[ProjectOffering]
(
   [ProjectOfferingId] INT IDENTITY(1,1) NOT NULL,
   [UnitOfferingId] INT NOT NULL,
   [ProjectId] INT NOT NULL,
   CONSTRAINT PK_ProjectOfferingId                 PRIMARY KEY (ProjectOfferingId),
   CONSTRAINT FK_ProjectOffering_UnitOfferingId    FOREIGN KEY (UnitOfferingId)    REFERENCES [UnitOffering] (UnitOfferingId),
   CONSTRAINT FK_ProjectOffering_ProjectId         FOREIGN KEY (ProjectId)         REFERENCES [Project] (ProjectId)
)
