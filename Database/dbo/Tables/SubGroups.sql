CREATE TABLE [dbo].[SubGroups]
(
	[Id] INT NOT NULL,
	[IdGroup] INT NOT NULL, 
    [Name] NVARCHAR(512) NOT NULL, 
    [ReplDate] DATETIME NULL, 
    CONSTRAINT PK_SubGroups PRIMARY KEY (ID),
	CONSTRAINT FK_SubGroups_Groups foreign key (IdGroup) references Groups (ID) 
)
