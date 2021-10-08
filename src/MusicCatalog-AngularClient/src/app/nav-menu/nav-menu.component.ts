import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

const TOKEN_KEY = 'jwt-token';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn = false;
  email!: string;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.isLoggedIn = !!this.authService.getToken(TOKEN_KEY);

    if (this.isLoggedIn) {
      const user = this.authService.getCurrentUser;
      //this.email = user.email;
    }
  }

  logout(): void {
    this.authService.logOut();
    window.location.reload();
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
