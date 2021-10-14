import { Component, OnInit } from '@angular/core';
import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';

@Component({
  selector: 'app-genre',
  templateUrl: './genre-list.component.html'
})
export class GenreListComponent implements OnInit {
  loading = false;
  genres: Genre[];

  constructor(private genreService: GenreService) {
    this.genres = [];
  }

  ngOnInit(): void {
    this.loading = true;
    this.getGenresList();
  }

  getGenresList() {
    this.genreService.getGenres().subscribe(response => {
      this.loading = false;
      this.genres = response;
    })
  }
}
