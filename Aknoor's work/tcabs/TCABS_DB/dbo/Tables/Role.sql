﻿CREATE TABLE [dbo].[Role]
(
   [RoleId] INT IDENTITY (1,1) NOT NULL,
   [Name] NVARCHAR(50) NOT NULL,
   CONSTRAINT PK_RoleId PRIMARY KEY (RoleId)
)