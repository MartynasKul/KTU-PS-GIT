import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ApiService } from '../api-service.service';
import { Sponsor } from '../models/sponsor.model';

@Component({
  selector: 'app-add-comment-modal',
  templateUrl: './add-comment-modal.component.html',
  styleUrls: ['./add-comment-modal.component.scss']
})
export class AddCommentModalComponent {
  newCommentForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isError = false;
  sponsor: Sponsor = {id: -1, sponsorName: '', telephoneNumber: '', address: '', email: '', description: ''}


  constructor(
    private dialogRef: MatDialogRef<AddCommentModalComponent>,
    @Inject(MAT_DIALOG_DATA) public sponsor_inj: Sponsor,
    private apiService: ApiService,
    private router: Router) {

      this.sponsor = sponsor_inj

    this.newCommentForm = new FormGroup({
      newComment: new FormControl('', [Validators.required]),
    });
  }

  submitForm() {
    if (this.newCommentForm.invalid) {
      return;
    }
    this.isLoading = true;
    
    this.apiService.addCommentForSponsor(this.sponsor.id, this.newCommentForm.value.newComment)
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
