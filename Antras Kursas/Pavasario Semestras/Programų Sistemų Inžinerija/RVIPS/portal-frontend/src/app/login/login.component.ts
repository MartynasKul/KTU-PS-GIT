import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../api-service.service';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service.service';
import { Role, User } from '../models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  [x: string]: any;
  loginForm: FormGroup = new FormGroup({});
  isUnauthorized: Boolean = false;
  isLoading: Boolean = false;
  userRoles: Role[] | undefined = []
 
constructor(private apiService: ApiService, private authService: AuthService, private router: Router) {
  this.loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)])
  });
}

  ngOnInit() {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    }

  }

  submitForm() {
    this.isLoading = true;
    this.apiService.login(this.loginForm.value.email, this.loginForm.value.password)
    .subscribe(response => {
      localStorage.setItem('token', response);
      this.authService.resetToken();

      this.apiService.getUser().subscribe(x => {
        if (x.organizationId === null) {
          this.router.navigate(['/create-organization']);
        } else {
          this.router.navigate(['/']);
        }
      })
    }, error => {
      this.isUnauthorized = true;
      console.error(error);
      this.isLoading = false;
    });
  }

  openRegister() {
    this.router.navigate(['/register']);
  }
}