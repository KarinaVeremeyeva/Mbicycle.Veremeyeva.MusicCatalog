﻿ALTER TABLE Albums
ADD CONSTRAINT FK_SongAlbum
FOREIGN KEY (SongId) REFERENCES Songs(SongId);