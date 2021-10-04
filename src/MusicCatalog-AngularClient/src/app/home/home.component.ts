import { Component, OnInit } from '@angular/core';
import { Song } from '../_models/song';
import {SongService} from '../_services/song.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  loading = false;
  songs: Song[] = [];

  constructor(private songService: SongService) { }

  ngOnInit(): void {
    this.loading = true;
    this.songService.getSongs().subscribe(
      response => {
        this.loading = false;
        this.songs = response;
      },
      err => {
        this.songs = JSON.parse(err.error).message();
      }
    )
  }
}
