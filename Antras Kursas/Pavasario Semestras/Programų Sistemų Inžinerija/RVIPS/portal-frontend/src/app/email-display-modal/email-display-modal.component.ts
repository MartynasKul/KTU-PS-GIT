import { Component, Inject, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmailDisplay } from '../models/emaildisplay.model';

@Component({
  selector: 'app-email-display-modal',
  templateUrl: './email-display-modal.component.html',
  styleUrls: ['./email-display-modal.component.scss']
})
export class EmailDisplayModalComponent {
  @Input() htmlContent: string = "";
  displayEmail!: EmailDisplay;

  constructor(
    private dialogRef: MatDialogRef<EmailDisplayModalComponent>, @Inject(MAT_DIALOG_DATA) public email: EmailDisplay) {
    this.htmlContent = email.body
    this.displayEmail = email
  }
  

  close() {
    this.dialogRef.close();
  }

}
