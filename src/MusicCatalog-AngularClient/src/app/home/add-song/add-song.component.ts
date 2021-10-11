import { Component, OnInit } from '@angular/core';
import { Song } from '../../_models/song';
import { SongService } from '../../_services/song.service';
import { GenreService} from '../../_services/genre.service';
import { AlbumService} from '../../_services/album.service';
import { PerformerService} from '../../_services/performer.service';
import { FormBuilder, Validators} from "@angular/forms";
import { Album} from '../../_models/album';
import { Performer } from '../../_models/performer';
import { Genre} from '../../_models/genre';

@Component({
  selector: 'app-add-song',
  templateUrl: './add-song.component.html'
})
export class AddSongComponent implements OnInit {
  song: Song = new Song();
  submitted = false;
  errorMessage = '';
  createForm;
  albums: Album[] = [];
  genres: Genre[] = [];
  performers: Performer[] = [];

  constructor(
    private songService: SongService,
    private albumService: AlbumService,
    private genreService: GenreService,
    private performerService: PerformerService,
    private formBuilder: FormBuilder)
  {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      albumId: [''],
      genreId: [''],
      performerId: ['']
    });
  }

  ngOnInit(): void {
    this.albumService.getAlbums().subscribe((response: Album[]) => {
      this.albums = response;
    });
    this.genreService.getGenres().subscribe((response: Genre[]) => {
      this.genres = response;
    });
    this.performerService.getPerformers().subscribe((response: Performer[]) => {
      this.performers = response;
    });
  }

  onSubmit(formData): void {
    this.songService.postSong(formData.value)
      .subscribe(response => {
        this.submitted = true;
      },
          err => {
        this.errorMessage = err;
      });
  }

}
