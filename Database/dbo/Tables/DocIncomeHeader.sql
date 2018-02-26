CREATE TABLE [dbo].[DocIncomeHeader]
(
	[Id] INT Identity NOT NULL, 
    [DocDate] DATE NOT NULL, 
	[DocNumber] NVARCHAR(64) NULL, 
    [IdStore] INT NOT NULL, 
    [Comment] NVARCHAR(250) NULL,
	[DocNumberInt] AS dbo.RightNumber(DocNumber),
    [IsCommited] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT PK_DocIncomeHeader PRIMARY KEY (Id),
	CONSTRAINT FK_DocIncomeHeader_Store  FOREIGN KEY (IdStore) references Stores (ID),
	 
)
