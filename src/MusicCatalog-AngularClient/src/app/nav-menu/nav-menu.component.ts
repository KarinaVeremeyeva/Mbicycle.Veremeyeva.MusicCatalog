import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AuthUser } from "../_models/auth-user";
import {stringify} from "@angular/compiler/src/util";

const TOKEN_KEY = 'jwt-token';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn = false;
  currentUser: AuthUser = new AuthUser();

  constructor(private authService: AuthService)
  { }

  ngOnInit(): void {
    this.isLoggedIn = !!this.authService.getToken(TOKEN_KEY);

    if (this.isLoggedIn) {
      this.authService.currentUser.subscribe(x => this.currentUser = x)
      console.log('isLoggedIn', this.isLoggedIn);
      //this.authService.currentUserValue.next(true);

      console.log('app-component', this.currentUser)
    }
  }

  logout(): void {
    this.authService.logOut();
    window.location.reload();
    this.isLoggedIn = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
