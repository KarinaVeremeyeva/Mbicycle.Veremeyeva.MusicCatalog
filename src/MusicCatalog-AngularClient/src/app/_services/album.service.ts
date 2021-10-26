import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Album } from '../_models/album';
import { environment } from '../../environments/environment';
import { ApiPaths } from '../enums/api-paths';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  webApiUrl = environment.webApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  getAlbums() {
    return this.http.get<Album[]>(`${this.webApiUrl}/${ApiPaths.Albums}`);
  }

  getAlbum(id: number) {
    return this.http.get<Album>(`${this.webApiUrl}/${ApiPaths.Albums}/${id}`);
  }

  postAlbum(album: Album): Observable<Album> {
    return this.http.post<Album>(
      `${this.webApiUrl}/${ApiPaths.Albums}`,
      JSON.stringify(album),
      this.httpOptions);
  }

  putAlbum(album: Album): Observable<Album> {
    return this.http.put<Album>(
      `${this.webApiUrl}/${ApiPaths.Albums}/${album.albumId}`,
      JSON.stringify(album),
      this.httpOptions);
  }

  deleteAlbum(id: number){
    const url = `${this.webApiUrl}/${ApiPaths.Albums}/${id}`
    return this.http.delete(url, this.httpOptions);
  }
}
