import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { User } from '../_models/user';
import { environment } from '../../environments/environment';
import { ApiPaths } from '../enums/api-paths';

@Injectable({
  providedIn: 'root'
})
// Provides methods to access protected resources
export class UserService {
  authApiUrl = environment.userApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  // Gets all users
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.authApiUrl}/${ApiPaths.Admin}`);
  }

  // Gets user by id
  getUser(id: string): Observable<User> {
    return this.http.get<User>(`${this.authApiUrl}/${ApiPaths.Admin}/${id}`);
  }

  // Updates user
  putUser(user: User): Observable<User> {
    return this.http.put<User>(
      `${this.authApiUrl}/${ApiPaths.Admin}/${user.id}`,
      JSON.stringify(user),
      this.httpOptions);
  }

  // Deletes user by id
  deleteUser(id: string) {
    return this.http.delete(
      `${this.authApiUrl}/${ApiPaths.Admin}/${id}`,
      this.httpOptions);
  }
}
