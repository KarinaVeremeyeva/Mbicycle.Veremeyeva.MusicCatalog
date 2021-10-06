import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { User } from '../_models/user';
import { LoginUser } from '../_models/login-user';
import { RegisterUser } from '../_models/register-user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private auth_api_path = 'http://localhost:2563/api/User/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json; charset=utf-8',
      'Accept': 'application/json'
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
  loginUser(user: LoginUser): Observable<any> {
    return this.http.post<LoginUser>(this.auth_api_path + 'login',
      JSON.stringify(user),
      this.httpOptions);
  }

  // Sends post-request to the api for user sign up
  registerUser(user: RegisterUser): Observable<any> {
    //let currentUser = { email: user.email, role: user.role, id: "" };
    //localStorage.setItem('currentUser', JSON.stringify(currentUser));

    return this.http.post<RegisterUser>(
      this.auth_api_path + 'register',
      JSON.stringify(user),
      this.httpOptions)
      /*.pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(currentUser);
        return user;
      }));*/
  }

  // Sends get-request the api for logging out
  logOut() {
    this.http.get(this.auth_api_path + 'logout');
  }
}
