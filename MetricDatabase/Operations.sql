CREATE TABLE [dbo].[Operations]
(
	[Name] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Application] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Operations_ToTable] FOREIGN KEY ([Application]) REFERENCES [Applications]([Name]) 
)
