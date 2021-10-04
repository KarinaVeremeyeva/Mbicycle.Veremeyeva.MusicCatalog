import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { User } from '../_models/user';

import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit, OnDestroy {
  users: User[] = [];
  currentUser: User = new User;
  currentUserSubscription: Subscription;
  loading = false;

  constructor(
    private authService: AuthService,
    private userService: UserService)
  {
    this.currentUserSubscription = this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    })
  }

  ngOnInit(): void {
    this.loading = true;
    this.loadUsers()
  }

  ngOnDestroy() {
    this.currentUserSubscription.unsubscribe();
  }

  private loadUsers() {
    this.userService.getUsers().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });
  }
}
