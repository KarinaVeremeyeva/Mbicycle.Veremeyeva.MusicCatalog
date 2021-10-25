import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../modal/modal.component';

import { Song } from '../_models/song';
import { SongService } from '../_services/song.service';
import { AuthUser } from '../_models/auth-user';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  loading = false;
  songs: Song[] = [];
  searchText: any;
  currentUser: AuthUser = new AuthUser();

  constructor(
    private songService: SongService,
    private authService: AuthService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loading = true;
    this.songService.getSongs().subscribe(response => {
      this.loading = false;
      this.songs = response;
    });
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  get isAuthorized() : AuthUser {
    return this.currentUser;
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
