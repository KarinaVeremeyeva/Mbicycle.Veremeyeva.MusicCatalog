import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../modal/modal.component';

import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';
import { AuthUser } from "../../_models/auth-user";

@Component({
  selector: 'app-user',
  templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  currentUser: AuthUser = new AuthUser();
  loading = false;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private dialog: MatDialog)
  {
    this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    })
  }

  ngOnInit(): void {
    this.loading = true;
    this.loadUsers()
  }

  private loadUsers() {
    this.userService.getUsers().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });
  }

  deleteUser(id: string): void {
    this.userService.deleteUser(id).subscribe(() => {
      this.users = this.users.filter(item => item.id !== id);
    });
  }

  openDialog(id: string): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: 'Are you sure you want to delete this user?'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteUser(id);
      }
    });
  }
}
