import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from "@angular/common/http";
import  { BehaviorSubject, Observable } from "rxjs";
import { User } from '../_models/user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private auth_api_path = 'http://localhost:2563/api/User/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json'
    })
  };
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(<string>localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // Gets current user
  public get getCurrentUser(): User {
    return this.currentUserSubject.value;
  }

  // Sends post-request to the api for user login
  loginUser(email: string, password: string): Observable<any> {
    return this.http.post<any>(
      this.auth_api_path + 'login',
      JSON.stringify({ email, password }),
      this.httpOptions);
  }

  // Sends post-request to the api for user sign up
  registerUser(email: string, password: string, role: string): Observable<any> {
    return this.http.post<any>(
      this.auth_api_path + 'register',
      JSON.stringify({ email, password, role }),
      this.httpOptions);
  }

  // Sends get-request the api for logging out
  logOut() {
    return this.http.get(this.auth_api_path + 'logout')
  }
}
