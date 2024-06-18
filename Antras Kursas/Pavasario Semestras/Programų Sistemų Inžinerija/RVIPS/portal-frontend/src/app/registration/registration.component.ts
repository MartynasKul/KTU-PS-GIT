import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ApiService} from "../api-service.service";
import {Router} from "@angular/router";
import { AuthService } from '../auth-service.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})


export class RegistrationComponent implements OnInit {
  registrationForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isDuplicate = false;
  notMatchingPassword = false;
  passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

  constructor(private apiService: ApiService, private authService: AuthService, private router: Router) {
    this.registrationForm = new FormGroup({
      name:  new FormControl('', [Validators.required]),
      surname:  new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.pattern(this.passwordRegex)]),
      repeatedPassword: new FormControl('', [Validators.required, Validators.minLength(8)])
    });

  }

  ngOnInit() {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    }

  }

  submitForm() {
    this.notMatchingPassword = false;
    if (this.registrationForm.value.password != this.registrationForm.value.repeatedPassword) {
      this.notMatchingPassword = true;
      return;
    }
    if (this.registrationForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.register(
      this.registrationForm.value.name,
      this.registrationForm.value.surname,
      this.registrationForm.value.email,
      this.registrationForm.value.password)
      .subscribe(() => {
        this.isDuplicate = false;
        this.router.navigate(['/login']);
      }, error => {
        this.isDuplicate = true;
        this.isLoading = false;
        console.error(error);
      });
  }
}
