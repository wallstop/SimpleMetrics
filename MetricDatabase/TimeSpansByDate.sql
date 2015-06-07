CREATE TABLE [dbo].[TimeSpansByDate]
(
	[Timestamp] DATETIME NOT NULL PRIMARY KEY, 
    [Duration] DECIMAL NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_TimeSpansByDate_ToTable] FOREIGN KEY ([Name]) REFERENCES [TimeSpans]([Name])
)
