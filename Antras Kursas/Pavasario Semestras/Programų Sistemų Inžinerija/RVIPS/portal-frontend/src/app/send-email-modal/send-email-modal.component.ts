import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, EmailValidator } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { ApiService } from '../api-service.service';
import { EmailDisplay } from '../models/emaildisplay.model';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';

@Component({
  selector: 'app-send-email-modal',
  templateUrl: './send-email-modal.component.html',
  styleUrls: ['./send-email-modal.component.scss']
})
export class SendEmailModalComponent implements OnInit {
  newEmailForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isError = false;
  public Editor = ClassicEditor;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<SendEmailModalComponent>,
    @Inject(MAT_DIALOG_DATA) public address: string,
    private apiService: ApiService,
    private router: Router) {
      this.newEmailForm = new FormGroup({
        receiverEmail:  new FormControl('', [Validators.required, Validators.email]),
        subject:  new FormControl('', [Validators.required]),
        body: new FormControl('', [Validators.required])
      });

      this.newEmailForm = this.formBuilder.group({
        receiverEmail: [address],
        body: '',
        subject: ''
      });
  }

  ngOnInit(): void {
  }

  submitForm() {
    if (this.newEmailForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.sendEmail(
      this.newEmailForm.value.receiverEmail,
      this.newEmailForm.value.subject,
      this.newEmailForm.value.body)
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

  public onReady(editor: any) {
    
  }

  public onChange({ editor }: ChangeEvent) {
    const data = editor.data.get()
    this.newEmailForm.value.body = data;
  }
}
