import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AuthService } from "../../_services/auth.service";

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html'
})
export class EditUserComponent implements OnInit {
  editForm: FormGroup;
  currentUser: User = new User();
  roles: string[] = [];
  submitted = false;
  errorMessage = '';

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router)
  {
    this.editForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      role: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.currentUser.id = this.route.snapshot.params['id'];
    this.userService.getUser(this.currentUser.id).subscribe((response: User) => {
      this.currentUser = response;
      this.editForm.controls["email"].setValue(this.currentUser.email);
      this.editForm.controls["role"].setValue(this.currentUser.role);
    });

    this.populateRolesDropDownList();
  }

  get formField() { return this.editForm.controls; }

  onSubmit(formData): void {
    this.submitted = true;
    if (this.editForm.invalid) {
      return;
    }

    formData.value.id = this.currentUser.id;
    this.userService.putUser(formData.value)
      .subscribe(() => {
        this.router.navigateByUrl('users').then();
      });
  }

  private populateRolesDropDownList() {
    this.authService.getAllRoles().subscribe((response: string[]) => {
      this.roles = response;
    });
  }
}
