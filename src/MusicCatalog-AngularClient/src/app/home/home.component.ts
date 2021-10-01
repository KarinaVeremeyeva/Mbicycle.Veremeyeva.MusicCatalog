import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  loading = false;
  users: User[] = [];

  constructor(private  userService: UserService) { }

  ngOnInit(): void {
    this.loading = true;
    this.userService.getUsers().subscribe(
      data => {
        this.loading = false;
        this.users = data;
      },
      err => {
        this.users = JSON.parse(err.error).message();
      }
    )
  }
}
