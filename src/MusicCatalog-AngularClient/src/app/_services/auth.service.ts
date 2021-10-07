import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { User } from '../_models/user';
import { LoginUser } from '../_models/login-user';
import { RegisterUser } from '../_models/register-user';

const TOKEN_KEY = 'Authorization';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private auth_api_path = 'http://localhost:2563/api/User/';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json; charset=utf-8',
      'Accept': 'application/json'
    }),
    observe: 'response' as 'body'
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
  loginUser(user: LoginUser): Observable<HttpResponse<LoginUser>> {
    return this.http.post<any>(
      this.auth_api_path + 'login',
      JSON.stringify(user),
      this.httpOptions);
  }

  // Sends post-request to the api for user sign up
  registerUser(user: RegisterUser): Observable<HttpResponse<RegisterUser>> {
    let token = this.httpOptions.headers.get("Authorization");
    localStorage.setItem('jwt-token', JSON.stringify(token));
    console.log(token);

    return this.http.post<any>(
      this.auth_api_path + 'register',
      JSON.stringify(user),
      this.httpOptions)
  }

  // Sends get-request the api for logging out
  logOut() {
    this.http.get(this.auth_api_path + 'logout');
  }

  // Gets token
  public getToken(): string {
    return <string>localStorage.getItem(TOKEN_KEY);
  }

  public retrieveToken(req) {
    if (req.headers.authorization
      && req.headers.authorization.split(' ')[0] === 'Bearer'){
      return req.headers.authorization.split(' ')[1];
    }
    else if (req.query && req.query.token) {
      return req.query.token;
    }
    console.log(req.headers.authorization)

    return null;
  }

  /*
  // Gets the value of the cookie with the specified name
  private getCookie(key: string) {
    return this.cookieService.get(key);
  }

  // Returns a map of key-value pairs for cookies that can be accessed.
  private setCookie(key: string, value: string) {
    return this.cookieService.set(key, value);
  }

  // Clear cookie specified by name
  private deleteCookie(name: string){
    return this.cookieService.delete(name);
  }

  private deleteAllCookies() {
    return this.cookieService.deleteAll();
  }*/
}
