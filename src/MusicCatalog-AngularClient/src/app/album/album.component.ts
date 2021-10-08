import { Component, OnInit } from '@angular/core';
import { Album } from '../_models/album';
import { AlbumService } from '../_services/album.service';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html'
})
export class AlbumComponent implements OnInit {
  public albums: Album[];

  constructor(private albumService: AlbumService) {
    this.albums = [];
  }

  ngOnInit(): void {
    this.getAlbumsList();
  }

  getAlbumsList() {
    this.albumService.getAlbums().subscribe(response => {
      this.albums = response;
    })
  }
}
