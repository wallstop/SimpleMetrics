CREATE TABLE [dbo].[CountsByDate]
(
	[Timestamp] DATETIME NOT NULL PRIMARY KEY, 
    [Count] DECIMAL NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_CountsByDate_ToTable] FOREIGN KEY ([Name]) REFERENCES [Counts]([Name])
)
