USE [MusicCatalogDatabase]
GO

INSERT INTO [MusicCatalogDatabase].[dbo].[Performers]
		([Name])
		VALUES
		('Performer1'),
		('Performer2'),
		('Performer3'),
		('Performer4'),
		('Performer5')

GO

INSERT INTO [MusicCatalogDatabase].[dbo].[Genres]
		([Name])
		VALUES
		('Genre1'),
		('Genre2'),
		('Genre3')
GO

INSERT INTO [MusicCatalogDatabase].[dbo].[Songs]
		([Name],
		[GenreId],
		[PerformerId])
		VALUES
		('Song1', 1, 1),
		('Song2', 2, 2),
		('Song3', 3, 3),
		('Song4', 1, 4),
		('Song5', 2, 5)
GO

INSERT INTO [MusicCatalogDatabase].[dbo].[Albums]
		([Name],
		[ReleaseDate],
		[SongId])
		VALUES
		('Album1', '2021-01-01', 1),
		('Album2', '2021-01-02', 2),
		('Album3', '2021-01-03', 3),
		('Album4', '2021-01-04', 4),
		('Album5', '2021-01-05', 5)

GO

