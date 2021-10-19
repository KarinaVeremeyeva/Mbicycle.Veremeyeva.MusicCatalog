import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Song } from '../../_models/song';
import { SongService } from '../../_services/song.service';
import { GenreService} from '../../_services/genre.service';
import { AlbumService} from '../../_services/album.service';
import { PerformerService} from '../../_services/performer.service';
import { Album} from '../../_models/album';
import { Performer } from '../../_models/performer';
import { Genre } from '../../_models/genre';

@Component({
  selector: 'app-edit-song',
  templateUrl: './edit-song.component.html'
})
export class EditSongComponent implements OnInit {
  currentSong: Song = new Song();
  editForm: FormGroup;
  albums: Album[] = [];
  genres: Genre[] = [];
  performers: Performer[] = [];
  submitted = false;
  errorMessage = '';

  constructor(
    private songService: SongService,
    private albumService: AlbumService,
    private genreService: GenreService,
    private performerService: PerformerService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router)
  {
    this.editForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      albumId: ['', [Validators.required]],
      genreId: ['', [Validators.required]],
      performerId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.currentSong.songId = this.route.snapshot.params['id'];
    this.songService.getSong(this.currentSong.songId).subscribe((response: Song) => {
      this.currentSong = response;
      this.editForm.controls["name"].setValue(this.currentSong.name);
      this.editForm.controls["albumId"].setValue(this.currentSong.albumId);
      this.editForm.controls["genreId"].setValue(this.currentSong.genreId);
      this.editForm.controls["performerId"].setValue(this.currentSong.performerId);
    });

    this.populateDropDownList();
  }

  get formField() { return this.editForm.controls; }

  onSubmit(formData): void {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }
    formData.value.songId = this.currentSong.songId;

    this.songService.putSong(formData.value)
      .subscribe(() => {
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
