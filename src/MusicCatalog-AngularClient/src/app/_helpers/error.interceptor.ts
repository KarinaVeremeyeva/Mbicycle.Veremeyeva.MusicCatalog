import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service'

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  // Checks http response errors
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retry(1),
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // handle client-side error
          errorMessage = `Error ${error.error.message}`;
        } else {
          // handle  server-side error
          errorMessage = `Error ${error.status}\nMessage: ${error.message}`
        }
        if (error.status === 401 || error.status === 403) {
          //this.authService.logOut();
        }
        console.log(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
}
