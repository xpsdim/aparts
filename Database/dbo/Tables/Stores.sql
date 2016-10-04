CREATE TABLE [dbo].[Stores]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Caption] NVARCHAR(16) NOT NULL, 
    [Storeman] NVARCHAR(450) NULL,
	CONSTRAINT [FK_Storeman_UserId] FOREIGN KEY ([Storeman]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE SET NULL
)
