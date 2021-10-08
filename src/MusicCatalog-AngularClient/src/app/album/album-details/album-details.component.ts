import { Component, OnInit } from '@angular/core';

import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-album-details',
  templateUrl: './album-details.component.html'
})
export class AlbumDetailsComponent implements OnInit {
  currentAlbum: Album = {
    name: '',
    releaseDate: '',
  };
  message = '';

  constructor(
    private albumService: AlbumService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.message = '';
    this.getAlbumById(this.route.snapshot.params.id);
  }

  getAlbumById(id: number) {
    this.albumService.getAlbum(id)
      .subscribe(response => {
        this.currentAlbum = response;
        console.log(response);
      })
  }

  updateAlbum() {
    this.albumService.putAlbum(this.currentAlbum)
      .subscribe(response => {
        console.log(response);
      })
  }

  deleteAlbum() {
    this.albumService.deleteAlbum(<number>this.currentAlbum.id)
      .subscribe(response => {
        console.log(response);
        this.router.navigate(['/albums']);
      })
  }
}
