﻿ALTER TABLE Songs
ADD CONSTRAINT FK_GenreSong
FOREIGN KEY (GenreId) REFERENCES Genres(GenreId) ON DELETE CASCADE;