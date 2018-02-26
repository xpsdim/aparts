CREATE TABLE [dbo].[DocIncomeDetails]
(
	[Id] INT Identity NOT NULL,
	[IdHeader] INT NOT NULL, 
    [IdStoreItem] INT NOT NULL, 
	[Amount] DECIMAL(18, 2) NULL, 
    [PriceIn] MONEY NULL, 
    [PriceOut] MONEY NULL,     
    CONSTRAINT PK_DocIncomeDetails PRIMARY KEY (ID),
	CONSTRAINT FK_DocIncomeDetails_Header foreign key (IdHeader) references DocIncomeHeader (ID), 
	CONSTRAINT FK_DocIncomeDetails_StoreItem foreign key (IdStoreItem) references StoreItems (ID) 
)
