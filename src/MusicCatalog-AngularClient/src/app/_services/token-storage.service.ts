import { Injectable } from '@angular/core';

const TOKEN_KEY = 'secret_jwt_key';
const USER_KEY = 'Authorization';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor() { }

  // Clears session after sign out
  public signOut(): void {
    window.sessionStorage.clear();
  }

  // Saves token
  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  // Gets token
  public getToken(): string {
    return sessionStorage.getItem(TOKEN_KEY)!;
  }

  // Saves user
  public saveUser(user): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  // Gets user
  public getUser(): any {
    return JSON.parse(sessionStorage.getItem(USER_KEY)!);
  }
}
