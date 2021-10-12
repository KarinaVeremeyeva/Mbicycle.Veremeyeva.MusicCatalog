import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Song } from '../_models/song';

@Injectable({
  providedIn: 'root'
})
export class SongService {
  private web_api_path = 'http://localhost:48517/api/songs/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  getSongs() {
    return this.http.get<Song[]>(this.web_api_path);
  }

  getSong(id: number) {
    return this.http.get<Song>(this.web_api_path);
  }

  postSong(song: Song): Observable<Song> {
    return this.http.post<Song>(
      this.web_api_path,
      JSON.stringify(song),
      this.httpOptions);
  }

  putSong(song: Song): Observable<Song> {
    return this.http.put<Song>(
      this.web_api_path + song.songId,
      JSON.stringify(song),
      this.httpOptions);
  }

  deleteSong(id: number) {
    return this.http.delete<Song>(
      this.web_api_path + id,
      this.httpOptions);
  }
}
