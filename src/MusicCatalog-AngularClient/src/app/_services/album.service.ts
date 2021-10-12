import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Album } from '../_models/album';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  private web_api_path = 'http://localhost:48517/api/albums/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }

  getAlbums() {
    return this.http.get<Album[]>(this.web_api_path);
  }

  getAlbum(id: number) {
    return this.http.get<Album>(this.web_api_path + id);
  }

  postAlbum(album: Album): Observable<Album> {
    return this.http.post<Album>(
      this.web_api_path,
      JSON.stringify(album),
      this.httpOptions);
  }

  putAlbum(album: Album): Observable<Album> {
    return this.http.put<Album>(
      this.web_api_path + album.albumId,
      JSON.stringify(album),
      this.httpOptions);
  }

  deleteAlbum(id: number){
    const url = `${this.web_api_path}${id}`
    return this.http.delete(url, this.httpOptions);
  }
}
