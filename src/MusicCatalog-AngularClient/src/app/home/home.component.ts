import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../modal/modal.component';

import { Song } from '../_models/song';
import { SongService } from '../_services/song.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  loading = false;
  songs: Song[] = [];
  searchText: any;

  constructor(
    private songService: SongService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loading = true;
    this.songService.getSongs().subscribe(response => {
      this.loading = false;
      this.songs = response;
    })
  }

  deleteSong(id: number): void {
    this.songService.deleteSong(id).subscribe(() => {
      this.songs = this.songs.filter(item => item.songId !== id);
    });
  }

  openDialog(id: number): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: $localize`Are you sure you want to delete this song?`
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteSong(id);
      }
    });
  }
}
