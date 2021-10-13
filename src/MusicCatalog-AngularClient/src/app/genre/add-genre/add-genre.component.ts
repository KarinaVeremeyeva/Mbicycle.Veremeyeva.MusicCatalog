import { Component, OnInit } from '@angular/core';

import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';

@Component({
  selector: 'app-add-genre',
  templateUrl: './add-genre.component.html'
})
export class AddGenreComponent implements OnInit {
  genre: Genre = new Genre();
  submitted = false;
  errorMessage = '';

  constructor(private genreService: GenreService) { }

  ngOnInit(): void {
  }

  createGenre(): void {
    this.genreService.postGenre(this.genre)
      .subscribe(() => {
        this.submitted = true;
      },
      err => {
        this.errorMessage = err;
      });
  }
}
