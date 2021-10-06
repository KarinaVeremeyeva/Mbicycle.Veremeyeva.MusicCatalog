import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

const TOKEN_KEY = 'secret_jwt_key';
const USER_KEY = 'Authorization';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor(private cookieService: CookieService) { }

  // Saves token
  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  // Gets token
  public getToken(): string {
    return this.getCookie(TOKEN_KEY);
  }

  public saveUser(user): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
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

  // Gets user
  public getUser(): any {
    return JSON.parse(sessionStorage.getItem(USER_KEY)!);
  }

  // Clears session after sign out
  public signOut(): void {
    window.sessionStorage.clear();
  }

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
  }
}
