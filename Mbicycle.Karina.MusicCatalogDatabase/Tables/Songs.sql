CREATE TABLE [dbo].[Songs]
(
	[SongId] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(15) NOT NULL,
	[GenreId] INT NOT NULL,
	[PerformerId] INT NOT NULL,
)
