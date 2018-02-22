CREATE TABLE [dbo].[StoreItems]
(
	[Id] INT NOT NULL, 
    [IdSubGroup] INT NOT NULL, 
    [CodesByCatalog] NVARCHAR(226) NULL, 
    [Price] MONEY NULL, 
    [ReplDate] DATETIME NULL,
	CONSTRAINT PK_StoreItems PRIMARY KEY (ID),
	CONSTRAINT FK_StoreItems_SubGroups foreign key (IdSubGroup) references SubGroups (ID) 
)
