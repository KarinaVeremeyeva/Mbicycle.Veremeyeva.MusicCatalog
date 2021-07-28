ALTER TABLE Song
ADD CONSTRAINT FK_PerformerSong
FOREIGN KEY (PerformerId) REFERENCES Performers(PerformerId);