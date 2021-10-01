import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from "@angular/common/http";
import  { BehaviorSubject, Observable } from "rxjs";
import { User } from '../_models/user';

const AUTH_API = 'http://localhost:2563/api/User/';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type' : 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(<string>localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // Sends post-request to the api for user login
  loginUser(email: string, password: string): Observable<any> {
    return this.http.post<any>(AUTH_API + 'login', { email, password }, httpOptions);
  }

  // Sends post-request to the api for user sign up
  registerUser(email: string, password: string, role: string): Observable<any> {
    return this.http.post<any>(AUTH_API + 'register', { email, password, role }, httpOptions);
  }

  // Sends get-request the api for logging out
  logOut() {
    return this.http.get(AUTH_API + 'logout')
  }

  // Gets current user
  getCurrentUser(): User {
    return this.currentUserSubject.value;
  }
}
