import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { map } from 'rxjs/operators';
import jwt_decode from 'jwt-decode';

import { LoginUser } from '../_models/login-user';
import { RegisterUser } from '../_models/register-user';
import { AuthUser } from '../_models/auth-user';
import { environment } from '../../environments/environment';
import { ApiPaths } from '../enums/api-paths';

const TOKEN_KEY = 'jwt-token';
const USER_KEY = 'current-user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  authApiUrl = environment.userApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json',
      'Accept': 'application/json'
    }),
    observe: 'response' as 'body'
  };

  private currentUserSubject: BehaviorSubject<AuthUser>;
  public currentUser: Observable<AuthUser>;

  constructor(
    private http: HttpClient,
    private cookieService: CookieService)
  {
    this.currentUserSubject = new BehaviorSubject<AuthUser>(
      JSON.parse(<string>localStorage.getItem(USER_KEY)));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // Gets current user
  public get currentUserValue(): AuthUser {
    return this.currentUserSubject.value;
  }

  // Sends post-request to the api for user login
  loginUser(user: LoginUser): Observable<HttpResponse<LoginUser>> {
    return this.http.post<any>(
      `${this.authApiUrl}/${ApiPaths.User}/login`,
      JSON.stringify(user),
      this.httpOptions)
      .pipe(map(res => {
        const token = res.headers.get('Authorization');
        if (token != null) {
          const currentUser = this.authorizeHandle(token);
          this.currentUserSubject.next(currentUser);
        }
        return res;
      }));
  }

  // Sends post-request to the api for user sign up
  registerUser(user: RegisterUser): Observable<HttpResponse<RegisterUser>> {
    return this.http.post<any>(
      `${this.authApiUrl}/${ApiPaths.User}/register`,
      JSON.stringify(user),
      this.httpOptions)
      .pipe(map(res => {
        const token = res.headers.get('Authorization');
        if (token != null) {
          const currentUser = this.authorizeHandle(token)
          this.currentUserSubject.next(currentUser);
        }
        return res;
      }));
  }

  private authorizeHandle(token): AuthUser {
    this.setCookie(TOKEN_KEY, token);
    const decodedToken = jwt_decode<string>(token);
    const decodedEmail = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    const decodedRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    const currentUser: AuthUser = {
      email: decodedEmail,
      role: decodedRole,
      token: token
    };
    localStorage.setItem(USER_KEY, JSON.stringify(currentUser));

    return currentUser;
  }

  // Sends get-request the api for logging out
  logOut() {
    localStorage.removeItem(USER_KEY);
    this.cookieService.delete(TOKEN_KEY);

    let user: any = null;
    this.currentUserSubject.next(user);
    this.http.get(`${this.authApiUrl}/${ApiPaths.User}/logout`);
  }

  //Gets all role names
  getAllRoles(): Observable<string[]> {
    const url = `${this.authApiUrl}/${ApiPaths.User}/getAllRoles`;
    return this.http.get<string[]>(url);
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
