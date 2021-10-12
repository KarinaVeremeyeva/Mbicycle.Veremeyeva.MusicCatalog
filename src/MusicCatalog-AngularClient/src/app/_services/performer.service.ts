import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Performer } from "../_models/performer";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PerformerService {
  private web_api_path = 'http://localhost:48517/api/performers/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }

  getPerformers() {
    return this.http.get<Performer[]>(this.web_api_path);
  }

  getPerformer(id: number){
    return this.http.get<Performer>(this.web_api_path + id);
  }

  postPerformer(performer: Performer): Observable<Performer> {
    return this.http.post<Performer>(
      this.web_api_path,
      JSON.stringify(performer),
      this.httpOptions);
  }

  putPerformer(performer: Performer): Observable<Performer> {
    return this.http.put<Performer>(
      this.web_api_path + performer.performerId,
      JSON.stringify(performer),
      this.httpOptions);
  }

  deletePerformer(id: number){
    return this.http.delete(
      this.web_api_path + id,
      this.httpOptions);
  }
}
