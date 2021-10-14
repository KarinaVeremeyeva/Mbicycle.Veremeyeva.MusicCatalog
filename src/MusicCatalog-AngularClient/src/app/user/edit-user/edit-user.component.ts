import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AuthService } from "../../_services/auth.service";

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent implements OnInit {
  currentUser: User = new User();
  submitted = false;
  errorMessage = '';
  editForm;
  roles: string[] = [];


  constructor(
    private userService: UserService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router)
  {
    this.editForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      role: ['']
    });
  }

  ngOnInit(): void {
    this.currentUser.id = this.route.snapshot.params['id'];
    this.userService.getUser(this.currentUser.id).subscribe((response: User) => {
      this.currentUser = response;
    });

    this.populateRolesDropDownList();
  }

  onSubmit(formData): void {
    formData.value.id = this.currentUser.id;

    this.userService.putUser(formData.value)
      .subscribe(() => {
        this.submitted = true;
        this.router.navigateByUrl('users').then();
      });
  }

  private populateRolesDropDownList() {
    this.authService.getAllRoles().subscribe((response: string[]) => {
      this.roles = response;
    });
  }
}
