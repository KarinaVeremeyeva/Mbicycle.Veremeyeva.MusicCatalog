CREATE TABLE [dbo].[Songs]
(
	[SongId] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(15) NOT NULL,
	[GenreId] INT NOT NULL,
	[PerformerId] INT NOT NULL,
<<<<<<< HEAD
	[AlbumId] INT NOT NULL,

=======
	[AlbumId] INT NOT NULL
>>>>>>> 6eaff59acdd126263cefa21c5ba05761cb15d388
)
