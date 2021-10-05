import { Component, OnInit } from '@angular/core';
import { Album } from '../../_models/album';
import {AlbumService} from '../../_services/album.service';

@Component({
  selector: 'app-add-album',
  templateUrl: './add-album.component.html'
})
export class AddAlbumComponent implements OnInit {
  public album: Album = new Album;
  submitted = false;
  errorMessage = '';

  constructor(private albumService: AlbumService ) { }

  ngOnInit(): void {
  }

  createAlbum(): void {
    this.albumService.postAlbum(this.album)
      .subscribe(response => {
        this.submitted = true;
      },
        err => {
          this.errorMessage = err;
        })
  }
}
