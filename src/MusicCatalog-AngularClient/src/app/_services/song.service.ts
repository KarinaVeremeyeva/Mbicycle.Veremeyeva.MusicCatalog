import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Song } from '../_models/song';
import { environment } from '../../environments/environment';
import { ApiPaths } from '../enums/api-paths';

@Injectable({
  providedIn: 'root'
})
export class SongService {
  webApiUrl = environment.webApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }

  getSongs() {
    return this.http.get<Song[]>(`${this.webApiUrl}/${ApiPaths.Song}`);
  }

  getSong(id: number) {
    return this.http.get<Song>(`${this.webApiUrl}/${ApiPaths.Song}/${id}`);
  }

  postSong(song: Song): Observable<Song> {
    return this.http.post<Song>(
          `${this.webApiUrl}/${ApiPaths.Song}`,
      JSON.stringify(song),
      this.httpOptions);
  }

  putSong(song: Song): Observable<Song> {
    return this.http.put<Song>(
      `${this.webApiUrl}/${ApiPaths.Song}/${song.songId}`,
      JSON.stringify(song),
      this.httpOptions);
  }

  deleteSong(id: number) {
    return this.http.delete<Song>(
      `${this.webApiUrl}/${ApiPaths.Song}/${id}`,
      this.httpOptions);
  }
}
