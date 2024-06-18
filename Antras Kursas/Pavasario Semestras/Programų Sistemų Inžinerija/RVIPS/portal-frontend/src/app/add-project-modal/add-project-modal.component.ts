import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiService } from '../api-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-project-modal',
  templateUrl: './add-project-modal.component.html',
  styleUrls: ['./add-project-modal.component.scss']
})
export class AddProjectModalComponent {
  registrationForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isDuplicate = false;

  constructor(
    private dialogRef: MatDialogRef<AddProjectModalComponent>,
    private apiService: ApiService,
    
    private router: Router) {
    this.registrationForm = new FormGroup({
      name:  new FormControl('', [Validators.required]),
      description:  new FormControl('', [Validators.required]),
      creationDate: new FormControl('', [Validators.required]),
      endOfProjectDate: new FormControl('', [Validators.required]),
    });
    
  }
  

  submitForm() {
    if (this.registrationForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.createProject(
      this.registrationForm.value.name,
      this.registrationForm.value.description,
      this.registrationForm.value.creationDate,
      this.registrationForm.value.endOfProjectDate)
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
