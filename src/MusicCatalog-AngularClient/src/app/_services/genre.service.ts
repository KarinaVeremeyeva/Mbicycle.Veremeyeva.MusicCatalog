import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Genre } from '../_models/genre';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private web_api_path = 'http://localhost:48517/api/genres/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  getGenres() {
    return this.http.get<Genre[]>(this.web_api_path);
  }

  getGenre(id: number) {
    return this.http.get<Genre>(this.web_api_path + id);
  }

  postGenre(genre: Genre): Observable<Genre> {
    return this.http.post<Genre>(
      this.web_api_path,
      JSON.stringify(genre),
      this.httpOptions);
  }

  putGenre(genre: Genre): Observable<Genre> {
    return this.http.put<Genre>(
      this.web_api_path + genre.genreId,
      JSON.stringify(genre),
      this.httpOptions);
  }

  deleteGenre(id: number){
    return this.http.delete(
      this.web_api_path + id,
      this.httpOptions);
  }
}
