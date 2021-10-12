import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthService } from '../_services/auth.service';
import { RegisterUser } from '../_models/register-user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  errorMessage = '';
  roles;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthService)
  {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      role: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.roles = this.authService.getAllRoles().subscribe((response: string[]) => {
      this.roles = response;
      console.log(response);
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
      .subscribe(()=> {
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]).then();
      },
      err => {
        this.errorMessage = err;
        this.loading = false;
      }
    );
  }
}
