import { Component } from '@angular/core';
import { ApiService } from '../api-service.service';
import { Email } from '../models/email.model';
import { OutgoingEmail } from '../models/outgoingemail.model';
import { MatDialog } from '@angular/material/dialog';
import { SendEmailModalComponent } from '../send-email-modal/send-email-modal.component';
import { EmailDisplayModalComponent } from '../email-display-modal/email-display-modal.component';
import { EmailDisplay } from '../models/emaildisplay.model';
import { Organization } from '../models/organization.model';

@Component({
  selector: 'app-emails',
  templateUrl: './emails.component.html',
  styleUrls: ['./emails.component.scss']
})
export class EmailsComponent {
  receivedEmails: Email[] = [];
  sentEmails: OutgoingEmail[] = [];
  displayedColumnsReceived: string[] = ['date', 'subject', 'fromName', 'fromEmail'];
  displayedColumnsSent: string[] = ['date', 'subject', 'fromName', 'receiverEmail'];
  displayEmail!: EmailDisplay;
  organization!: Organization;
  canSendEmail: boolean = false;
  constructor(private apiService: ApiService, private dialog: MatDialog) {
 
  }

  ngOnInit(): void {
    this.getOrganizationReceivedEmails()
    this.getOrganizationSentEmails()
    this.getOrganizationInfo()
  }

  getOrganizationInfo() {
    this.apiService.getOrganization()
    .subscribe(org => {
      this.organization = org;
      this.canSendEmail = org.smtpServerPort !== null
    });
  }

  getOrganizationReceivedEmails() {
    this.apiService.getOrganizationReceivedEmailList()
    .subscribe(emails => {
      this.receivedEmails = emails
    });
  }

  getOrganizationSentEmails() {
    this.apiService.getOrganizationSentEmailList()
    .subscribe(emails => {
      this.sentEmails = emails
    });
  }

  openModal() {
    var dialogRef = this.dialog.open(SendEmailModalComponent);

    dialogRef.afterClosed().subscribe(() => this.getOrganizationSentEmails());
  }

  openEmailReadModal(email: Email) {
    var letter = email.htmlBody

    if (!email.htmlBody) {
      letter = email.textBody
    }

    this.displayEmail = {
      subject: email.subject,
      from: `${email.fromEmail} (${email.fromName})`,
      to: `Ši organizacija`,
      body: letter,
      date: email.date
    };

    const dialogRef = this.dialog.open(EmailDisplayModalComponent, {
      data: this.displayEmail
    });
  }

  openEmailSentModal(email: OutgoingEmail) {
    this.displayEmail = {
      subject: email.subject,
      from: `${email.user.name} (iš organizacijos vidaus)`,
      to: email.receiverEmail,
      body: email.body,
      date: email.date
    };

    const dialogRef = this.dialog.open(EmailDisplayModalComponent, {
      data: this.displayEmail
    });
  }
}
