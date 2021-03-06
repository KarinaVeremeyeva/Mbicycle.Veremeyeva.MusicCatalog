import  { Injectable } from '@angular/core';
import  { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthService } from '../_services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate{
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  // Prevents unauthenticated users from accessing restricted routes
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authService.currentUserValue;
    if (currentUser) {
      if (route.data.roles && route.data.roles.indexOf(currentUser.role) === -1) {
        this.router.navigate(['/']).then();
        return false;
      }
      return true;
    }

    // redirect not logged user to login page
    this.router.navigate(['/login'],
      { queryParams: { returnUrl: state.url }}).then();
    return false;
  }
}

