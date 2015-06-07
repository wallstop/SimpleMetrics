CREATE TABLE [dbo].[TimeSpans]
(
	[Name] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Operation] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_TimeSpans_ToTable] FOREIGN KEY ([Operation]) REFERENCES [Operations]([Name])
)
