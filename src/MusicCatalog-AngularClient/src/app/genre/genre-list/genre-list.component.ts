import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../modal/modal.component';

import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';

@Component({
  selector: 'app-genre',
  templateUrl: './genre-list.component.html'
})
export class GenreListComponent implements OnInit {
  genres: Genre[] = [];
  loading = false;

  constructor(
    private genreService: GenreService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loading = true;
    this.getGenresList();
  }

  getGenresList() {
    this.genreService.getGenres().subscribe(response => {
      this.loading = false;
      this.genres = response;
    });
  }

  deleteGenre(id: number): void {
    this.genreService.deleteGenre(id).subscribe(() => {
      this.genres = this.genres.filter(item => item.genreId !== id);
    });
  }

  openDialog(id: number): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: $localize`Are you sure you want to delete this genre?`
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteGenre(id);
      }
    });
  }
}
