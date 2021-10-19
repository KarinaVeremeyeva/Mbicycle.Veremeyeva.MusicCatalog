import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../_services/auth.service';
import { AuthUser } from '../_models/auth-user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  currentUser: AuthUser = new AuthUser();

  constructor(
    private authService: AuthService,
    private router: Router)
  { }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === 'admin';
  }

  logout(): void {
    this.authService.logOut();
    this.router.navigate(['/']).then();
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
