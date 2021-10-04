import { Component, OnInit } from '@angular/core';
import { Album } from '../_models/album';
import { AlbumService } from '../_services/album.service';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html'
})
export class AlbumComponent implements OnInit {
  public albums: Album[];

  constructor(private service: AlbumService) {
    this.albums = [];
  }

  ngOnInit(): void {
    this.getAlbumsList();
  }

  getAlbumsList() {
    this.service.getAlbums().subscribe(response => {
      console.log(response);
      this.albums = response;
    })
  }
}
