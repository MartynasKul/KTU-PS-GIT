import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiService } from '../api-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.scss']
})
export class AddUserModalComponent {
  registrationForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isDuplicate = false;
  notMatchingPassword = false;
  passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

  constructor(
    private dialogRef: MatDialogRef<AddUserModalComponent>,
    private apiService: ApiService,
    private router: Router) {
    this.registrationForm = new FormGroup({
      name:  new FormControl('', [Validators.required]),
      surname:  new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.pattern(this.passwordRegex)]),
      repeatedPassword: new FormControl('', [Validators.required, Validators.minLength(8)])
    });
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
    this.apiService.addUserToOrganization(
      this.registrationForm.value.name,
      this.registrationForm.value.surname,
      this.registrationForm.value.email,
      this.registrationForm.value.password)
      .subscribe(() => {
        this.isDuplicate = false;
        this.dialogRef.close();
      }, error => {
        this.isDuplicate = true;
        this.isLoading = false;
        console.error(error);
      });
  }

  close() {
    this.dialogRef.close();
  }
}
