import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from '../_services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  // Inspects and transforms HTTP requests before they are sent to server
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = this.authService.getCurrentUser;
    let token = <string>localStorage.getItem('token');
    request = this.addToken(request)

    return next.handle(request);
  }

  private addToken(req: HttpRequest<any>): HttpRequest<any> {
    //let token = req.headers.get("Authorization")
    return req.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.getToken()}`
      }
    })
  }
}
