import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { AuthService } from './_services/auth.service';
import { AuthUser } from './_models/auth-user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
  currentUser: AuthUser = new AuthUser();

  constructor(
    private router: Router,
    private authService: AuthService)
  {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
  }

  ngOnInit(): void {
  }

  logout(): void {
    this.authService.logOut();
  }
}
