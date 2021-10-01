import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup } from '@angular/forms';

import { AuthService } from '../_services/auth.service';
import { TokenStorageService } from '../_services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  errorMessage = '';

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private tokenStorage: TokenStorageService,
    private router: Router
  ) {
    // Redirect to home if user already logged in
    if (this.authService.currentUser){
      this.router.navigate(['/'])
    }
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: ['']
    });
  }

  get formField() { return this.loginForm.controls; }

  onSubmit(): void {
    this.submitted = true;
    this.loading = true;

    this.authService.loginUser(this.formField.email.value, this.formField.password.value)
      .subscribe(
      data => {
        this.tokenStorage.saveToken(data.token);
        this.tokenStorage.saveUser(data);
        this.reloadPage();
      },
      err => {
        this.errorMessage = err;
        this.loading = false;
      }
    );
  }

  reloadPage(): void {
    window.location.reload();
  }

}
