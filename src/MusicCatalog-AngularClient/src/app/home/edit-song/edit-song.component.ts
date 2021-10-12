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
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-edit-song',
  templateUrl: './edit-song.component.html'
})
export class EditSongComponent implements OnInit {
  song: Song = new Song();
  submitted = false;
  errorMessage = '';
  editForm;
  albums: Album[] = [];
  genres: Genre[] = [];
  performers: Performer[] = [];

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
      songId: [''],
      name: ['', Validators.required],
      albumId: [''],
      genreId: [''],
      performerId: ['']
    });
  }

  ngOnInit(): void {
    this.song.songId = this.route.snapshot.params['id'];

    this.songService.getSong(this.song.songId).subscribe((response: Song) => {
      this.song = response;
    });

    this.populateDropDownList();
  }

  onSubmit(formData): void {
    this.songService.putSong(formData.value)
      .subscribe(response => {
        this.submitted = true;
        this.router.navigateByUrl('songs')
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
