import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AuthUser } from '../_models/auth-user';

const TOKEN_KEY = 'jwt-token';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  currentUser: AuthUser = new AuthUser();

  constructor(private authService: AuthService)
  { }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x)
  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === 'admin';
  }

  logout(): void {
    this.authService.logOut();
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
