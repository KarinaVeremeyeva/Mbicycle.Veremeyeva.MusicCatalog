import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../modal/modal.component';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';

@Component({
  selector: 'app-album',
  templateUrl: './album-list.component.html'
})
export class AlbumListComponent implements OnInit {
  albums: Album[] = [];

  constructor(
    private albumService: AlbumService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getAlbumsList();
  }

  getAlbumsList() {
    this.albumService.getAlbums().subscribe(response => {
      this.albums = response;
    });
  }

  deleteAlbum(id: number): void {
    this.albumService.deleteAlbum(id).subscribe(() => {
      this.albums = this.albums.filter(item => item.albumId !== id);
    });
  }

  openDialog(id: number): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: 'Are you sure you want to delete this album?'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteAlbum(id);
      }
    });
  }
}
