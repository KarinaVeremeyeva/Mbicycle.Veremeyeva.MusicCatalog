import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Song } from '../../_models/song';
import { SongService } from '../../_services/song.service';
import { GenreService} from '../../_services/genre.service';
import { AlbumService} from '../../_services/album.service';
import { PerformerService} from '../../_services/performer.service';
import { Album} from '../../_models/album';
import { Performer } from '../../_models/performer';
import { Genre} from '../../_models/genre';

@Component({
  selector: 'app-add-song',
  templateUrl: './add-song.component.html'
})
export class AddSongComponent implements OnInit {
  song: Song = new Song();
  createForm: FormGroup;
  submitted = false;
  albums: Album[] = [];
  genres: Genre[] = [];
  performers: Performer[] = [];

  constructor(
    private songService: SongService,
    private albumService: AlbumService,
    private genreService: GenreService,
    private performerService: PerformerService,
    private router: Router,
    private formBuilder: FormBuilder)
  {
    this.createForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      albumId: ['', [Validators.required]],
      genreId: ['', [Validators.required]],
      performerId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.populateDropDownList();
  }

  get formField() { return this.createForm.controls; }

  onSubmit(formData): void {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }

    this.songService.postSong(formData.value).subscribe(() => {
      this.router.navigateByUrl('songs').then();
    });
  }

  private populateDropDownList() {
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
}
