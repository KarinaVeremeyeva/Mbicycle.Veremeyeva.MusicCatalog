import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute  } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';
import { LoginUser } from "../_models/login-user";

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
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Redirect to home if user already logged in
    if (this.authService.getCurrentUser){
      this.router.navigate(['/'])
    }
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  get formField() { return this.loginForm.controls; }

  onSubmit(): void {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;

    let user: LoginUser = {
      email: this.formField.email.value,
      password: this.formField.password.value
    }

    this.authService.loginUser(user)
      .pipe(first())
      .subscribe(
        (data: HttpResponse<LoginUser>) => {
          let token = data.headers.get('Authorization')
          console.log(token);
          if (typeof token === "string") {
            localStorage.setItem('my-token', token);
          }

          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
          },
        err => {
          this.errorMessage = err;
          this.loading = false;
        }
    );
  }
}

