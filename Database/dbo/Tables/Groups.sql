﻿CREATE TABLE [dbo].[Groups]
(
	[Id] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [ReplDate] DATETIME NULL,
	CONSTRAINT PK_Groups PRIMARY KEY (ID)
)
