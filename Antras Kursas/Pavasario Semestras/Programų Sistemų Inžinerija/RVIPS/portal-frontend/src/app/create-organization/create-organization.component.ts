import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../api-service.service';
import { AuthService } from '../auth-service.service';

@Component({
  selector: 'app-create-organization',
  templateUrl: './create-organization.component.html',
  styleUrls: ['./create-organization.component.scss']
})
export class CreateOrganizationComponent implements OnInit {
  registrationForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;

  constructor(private apiService: ApiService, private router: Router) {
    this.registrationForm = new FormGroup({
      name:  new FormControl('', [Validators.required]),
    });

  }

  ngOnInit() {
  }

  submitForm() {
    if (this.registrationForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.createOrganization(this.registrationForm.value.name)
      .subscribe(() => {
        this.router.navigate(['/']);
      }, error => {
        this.isLoading = false;
        console.error(error);
      });
  }
}
