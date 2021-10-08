import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
// Provides methods to access protected resources
export class UserService {
  private admin_api_path = 'http://localhost:2563/api/Admin/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  // Gets all users
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.admin_api_path);
  }

  // Gets user by id
  getUser(id: string): Observable<User> {
    return this.http.get<User>(this.admin_api_path + id);
  }

  // Updates user
  putUser(user: User): Observable<User> {
    return this.http.put<User>(
      this.admin_api_path + user.id,
      JSON.stringify(user),
      this.httpOptions);
  }

  // Deletes user by id
  deleteUser(id: string) {
    return this.http.delete(this.admin_api_path + id, this.httpOptions);
  }
}
