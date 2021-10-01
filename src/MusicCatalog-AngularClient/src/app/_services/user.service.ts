import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

const ADMIN_API_URL = 'http://localhost:2563/api/Admin/';

@Injectable({
  providedIn: 'root'
})
// Provides methods to access protected resources
export class UserService {

  constructor(private http: HttpClient) { }

  // Gets all users
  getUsers(): Observable<any> {
    return this.http.get<User[]>(ADMIN_API_URL);
  }

  // Gets user by id
  getUser(id: string): Observable<any> {
    return this.http.get<User>(ADMIN_API_URL + id);
  }

  // Updates user
  putUser(credentials): Observable<any> {
    return this.http.put(ADMIN_API_URL + credentials.id, credentials);
  }

  // Deletes user by id
  deleteUser(id: string): Observable<any> {
    return this.http.delete(ADMIN_API_URL + id);
  }


}
