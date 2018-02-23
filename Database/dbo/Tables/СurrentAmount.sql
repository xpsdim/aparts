CREATE TABLE [dbo].[СurrentAmount]
(
	[IdStoreItem] INT NOT NULL , 
    [IdStore] INT NOT NULL,	 
    [Amount] DECIMAL(18, 2) NOT NULL, 
    CONSTRAINT PK_СurrentAmount PRIMARY KEY NONCLUSTERED ([IdStoreItem], [IdStore]),
	CONSTRAINT FK_СurrentAmount_Store foreign key (IdStore) references Stores (ID),
	CONSTRAINT FK_СurrentAmount_StoreItem foreign key (IdStoreItem) references Stores (ID),
	Index IDX_IdStoreItem CLUSTERED (IdStoreItem)
)
