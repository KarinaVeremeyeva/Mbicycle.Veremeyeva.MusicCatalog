CREATE TABLE [dbo].[Albums]
(
	[AlbumId] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(30) NOT NULL,
	[ReleaseDate] DATE NOT NULL,
)
