import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Performer } from '../_models/performer';
import { environment } from '../../environments/environment';
import { ApiPaths } from '../enums/api-paths';

@Injectable({
  providedIn: 'root'
})
export class PerformerService {
  webApiUrl = environment.webApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }

  getPerformers() {
    return this.http.get<Performer[]>(`${this.webApiUrl}/${ApiPaths.Performers}`);
  }

  getPerformer(id: number){
    return this.http.get<Performer>(`${this.webApiUrl}/${ApiPaths.Performers}/${id}`);
  }

  postPerformer(performer: Performer): Observable<Performer> {
    return this.http.post<Performer>(
      `${this.webApiUrl}/${ApiPaths.Performers}`,
      JSON.stringify(performer),
      this.httpOptions);
  }

  putPerformer(performer: Performer): Observable<Performer> {
    return this.http.put<Performer>(
      `${this.webApiUrl}/${ApiPaths.Performers}/${performer.performerId}`,
      JSON.stringify(performer),
      this.httpOptions);
  }

  deletePerformer(id: number){
    return this.http.delete(
      `${this.webApiUrl}/${ApiPaths.Performers}/${id}`,
      this.httpOptions);
  }
}
