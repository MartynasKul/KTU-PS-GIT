import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { ApiService } from '../api-service.service';
import { MatTableDataSource } from '@angular/material/table';
import { User } from '../models/user.model'
import { MatDialog } from '@angular/material/dialog';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { Organization } from '../models/organization.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Sponsor } from '../models/sponsor.model'
import { Email } from '../models/email.model';
import { EmailDisplay } from '../models/emaildisplay.model';
import { OutgoingEmail } from '../models/outgoingemail.model';
import { ActivatedRoute, Router } from '@angular/router';
import { EmailDisplayModalComponent } from '../email-display-modal/email-display-modal.component';
import { SendEmailModalComponent } from '../send-email-modal/send-email-modal.component';
import { SponsorComment } from '../models/comment.model';
import { AddCommentModalComponent } from '../add-comment-modal/add-comment-modal.component';

@Component({
  selector: 'app-business-info',
  templateUrl: './business-info.component.html',
  styleUrls: ['./business-info.component.scss']
})
export class BusinessInfoComponent implements OnInit {
  title: string  = ' RVIPS ';
  receivedEmails: Email[] = [];
  sentEmails: OutgoingEmail[] = [];
  displayedColumnsReceived: string[] = ['date', 'subject', 'fromName', 'fromEmail'];
  displayedColumnsSent: string[] = ['date', 'subject', 'fromName', 'receiverEmail'];
  displayEmail!: EmailDisplay;
  id: string | null = '';
  sponsor: Sponsor = {id: -1, sponsorName: '', telephoneNumber: '', address: '', email: '', description: ''}
  active: boolean = false;
  canSendEmail: boolean = false;
  comments: SponsorComment[] = [];

  constructor(private apiService: ApiService, private dialog: MatDialog, private route: ActivatedRoute, private router: Router) {
 
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.getOrganizationInfo()
    this.getSponsorInfo()
    this.getComments()
  }

  getOrganizationReceivedEmails() {
    this.apiService.getOrganizationReceivedEmailListByAddress(this.sponsor.email)
    .subscribe(emails => {
      this.receivedEmails = emails;
    });
  }

  getOrganizationSentEmails() {
    this.apiService.getOrganizationSentEmailListByAddress(this.sponsor.email)
    .subscribe(emails => {
      this.sentEmails = emails;
    });
  }

  getOrganizationInfo() {
    this.apiService.getOrganization()
    .subscribe(org => {
      this.canSendEmail = org.smtpServerPort !== null
    });
  }

  getSponsorInfo() {
    this.apiService.getSponsor(parseInt(this.id ?? '0'))
    .subscribe(sponsor => {
      this.sponsor = sponsor;
      this.getOrganizationReceivedEmails()
      this.getOrganizationSentEmails()
      this.active = true
      if (this.sponsor.id == 0) {
        this.router.navigate(['/not-found'])
      }
    })
  }

  sendEmailModal() {
    var dialogRef = this.dialog.open(SendEmailModalComponent, {
      data: this.sponsor.email
    });
    dialogRef.afterClosed().subscribe(() => this.getSponsorInfo());
  }

  updateTitle(value: string){
    this.title = value;
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
  
  getComments() {
      this.apiService.getSponsorComments(Number(this.id)).subscribe(x => {this.comments = x})
      
    }

    openCommentCreationModal() {
      var dialogRef = this.dialog.open(AddCommentModalComponent, {
        data: this.sponsor
      });
  
      dialogRef.afterClosed().subscribe(() => this.getComments());
    }
}
