import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { map } from 'rxjs/operators';

import { LoginUser } from '../_models/login-user';
import { RegisterUser } from '../_models/register-user';

const TOKEN_KEY = 'jwt-token';

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
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService)
  {
    this.currentUserSubject = new BehaviorSubject<any>(this.getToken(TOKEN_KEY));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // Gets current user
  public get getCurrentUser(): any {
    return this.currentUserSubject.value;
  }

  // Sends post-request to the api for user login
  loginUser(user: LoginUser): Observable<HttpResponse<LoginUser>> {
    return this.http.post<any>(
      this.auth_api_path + 'login',
      JSON.stringify(user),
      this.httpOptions)
      .pipe(map(res => {
        const token = res.headers.get('Authorization');
        if (token != null) {
          this.setCookie(TOKEN_KEY, token);
        }
        this.currentUserSubject.next(res);
        return res;
      }));
  }

  // Sends post-request to the api for user sign up
  registerUser(user: RegisterUser): Observable<HttpResponse<RegisterUser>> {
    return this.http.post<any>(
      this.auth_api_path + 'register',
      JSON.stringify(user),
      this.httpOptions)
      .pipe(map(res => {
        const token = res.headers.get('Authorization');
        if (token != null) {
          this.setCookie(TOKEN_KEY, token);
          console.log("access-token: " + token);
        }
        this.currentUserSubject.next(res);
        return res;
      }));
  }

  // Sends get-request the api for logging out
  logOut() {
    this.cookieService.deleteAll();
    this.currentUserSubject.next(null);
    return this.http.get(this.auth_api_path + 'logout');
  }


  //Gets all role names
  getAllRoles(): Observable<string[]> {
    return this.http.get<string[]>(this.auth_api_path + 'getAllRoles');
  }

  // Gets token from cookie
  public getToken(key: string): string {
    return this.cookieService.get(key);
  }

  // Sets cookie value by key
  private setCookie(key: string, value: string) {
    return this.cookieService.set(key, value);
  }
}
