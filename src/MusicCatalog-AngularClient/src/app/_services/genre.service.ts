import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Genre } from '../_models/genre';
import { environment } from '../../environments/environment';
import { ApiPaths } from '../enums/api-paths';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  webApiUrl = environment.webApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  getGenres() {
    return this.http.get<Genre[]>(`${this.webApiUrl}/${ApiPaths.Genres}`);
  }

  getGenre(id: number) {
    return this.http.get<Genre>(`${this.webApiUrl}/${ApiPaths.Genres}/${id}`);
  }

  postGenre(genre: Genre): Observable<Genre> {
    return this.http.post<Genre>(
      `${this.webApiUrl}/${ApiPaths.Genres}`,
      JSON.stringify(genre),
      this.httpOptions);
  }

  putGenre(genre: Genre): Observable<Genre> {
    return this.http.put<Genre>(
      `${this.webApiUrl}/${ApiPaths.Genres}/${genre.genreId}`,
      JSON.stringify(genre),
      this.httpOptions);
  }

  deleteGenre(id: number){
    return this.http.delete(
      `${this.webApiUrl}/${ApiPaths.Genres}/${id}`,
      this.httpOptions);
  }
}
