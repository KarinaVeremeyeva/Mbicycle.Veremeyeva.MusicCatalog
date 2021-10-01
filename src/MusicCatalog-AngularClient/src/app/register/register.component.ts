import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { AuthService} from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  errorMessage = '';

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: [''],
      password: [''],
      role: ['']
    });
  }

  get formField() { return this.registerForm.controls; }

  onSubmit(): void {
    this.authService.registerUser(
      this.formField.email.value,
      this.formField.role.value,
      this.formField.role.value
    ).subscribe(
      data => {
        console.log(data);
      },
      err => {
        this.errorMessage = err;
        this.loading = false;
      }
    );
  }

}
