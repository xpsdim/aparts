CREATE TABLE [dbo].[UserVisibleStores]
(
	[UserId] NVARCHAR(450) NOT NULL , 
    [StoreId] INT NOT NULL, 
    PRIMARY KEY ([UserId], [StoreId]),
	CONSTRAINT [FK_VisibleStore_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_VisibleStore_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Stores] ([Id]) ON DELETE CASCADE
)
