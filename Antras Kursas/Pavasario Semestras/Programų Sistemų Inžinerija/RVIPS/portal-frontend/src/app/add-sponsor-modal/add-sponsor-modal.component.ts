import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { ApiService } from '../api-service.service';
import { AddSponsorRequest } from '../models/addsponsor.model';

@Component({
  selector: 'app-add-sponsor-modal',
  templateUrl: './add-sponsor-modal.component.html',
  styleUrls: ['./add-sponsor-modal.component.scss']
})
export class AddSponsorModalComponent {
  newSponsorForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isError = false;
  request!: AddSponsorRequest;


  constructor(
    private dialogRef: MatDialogRef<AddSponsorModalComponent>,
    private apiService: ApiService,
    private router: Router) {
    this.newSponsorForm = new FormGroup({
      sponsorName: new FormControl('', [Validators.required]),
      email:  new FormControl('', [Validators.required, Validators.email]),
      telephoneNumber:  new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required]),
      description:  new FormControl('', [Validators.required])
    });
  }

  submitForm() {
    if (this.newSponsorForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.request = {
      sponsorName: this.newSponsorForm.value.sponsorName,
      telephoneNumber: this.newSponsorForm.value.telephoneNumber,
      address: this.newSponsorForm.value.address,
      email: this.newSponsorForm.value.email,
      description: this.newSponsorForm.value.description
    }
    this.apiService.addSponsor(this.request)
      .subscribe(() => {
        this.isError = false;
        this.dialogRef.close();
      }, error => {
        this.isError = true;
        this.isLoading = false;
        console.error(error);
      });
  }

  close() {
    this.dialogRef.close();
  }
}
