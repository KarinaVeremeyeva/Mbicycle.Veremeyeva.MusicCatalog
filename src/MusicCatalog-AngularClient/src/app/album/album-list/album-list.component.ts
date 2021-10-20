import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { ModalComponent } from '../../modal/modal.component';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';

@Component({
  selector: 'app-album',
  templateUrl: './album-list.component.html'
})
export class AlbumListComponent implements OnInit {
  public albums: Album[];
  dialogRef!: MatDialogRef<ModalComponent>;

  constructor(
    private albumService: AlbumService,
    public dialog: MatDialog)
  {
    this.albums = [];
  }

  ngOnInit(): void {
    this.getAlbumsList();
  }

  getAlbumsList() {
    this.albumService.getAlbums().subscribe(response => {
      this.albums = response;
    });
  }

  openDialog(id: number): void {
    this.dialogRef = this.dialog.open(ModalComponent, {
      width: '350px',
      data: 'Are you sure you want to delete this album?'
    });

    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Yes clicked');
        this.albumService.deleteAlbum(id).subscribe(() => {
          this.albums = this.albums.filter(item => item.albumId !== id);
        });
      }
    })
  }
}
