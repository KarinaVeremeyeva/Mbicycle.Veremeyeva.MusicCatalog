import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpResponse} from '@angular/common/http';
import { first } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';
import { RegisterUser } from '../_models/register-user';

const TOKEN_KEY = 'jwt-token';

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
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      role: ['', [Validators.required]]
    });
  }

  get formField() { return this.registerForm.controls; }

  onSubmit(): void {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    let user: RegisterUser = {
      email: this.formField.email.value,
      password: this.formField.password.value,
      role: this.formField.role.value
    };

    this.authService.registerUser(user)
      .pipe(first())
      .subscribe((data: HttpResponse<RegisterUser>)=> {
          const token = data.headers.get("Authorization");
          if (token != null){
            this.authService.setCookie(TOKEN_KEY, token);
            console.log(token);
          }
          data.headers.keys().map( (key) => console.log(`${key}: ${data.headers.get(key)}`));

          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]).then(r => console.log(r));
      },
      err => {
        this.errorMessage = err;
        this.loading = false;
      }
    );
  }
}
