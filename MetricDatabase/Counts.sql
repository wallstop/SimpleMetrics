CREATE TABLE [dbo].[Counts]
(
	[Name] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Operation] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Counts_ToTable] FOREIGN KEY ([Operation]) REFERENCES [Operations]([Name])
)
