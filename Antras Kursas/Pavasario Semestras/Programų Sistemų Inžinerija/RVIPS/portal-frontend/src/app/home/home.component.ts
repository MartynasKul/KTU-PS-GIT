import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { ApiService } from '../api-service.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup } from '@angular/forms';
import { Sponsor } from '../models/sponsor.model'
import { EmailDisplayModalComponent } from '../email-display-modal/email-display-modal.component';
import { Email } from '../models/email.model';
import { EmailDisplay } from '../models/emaildisplay.model';
import { Project } from '../models/project.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title: string  = ' RVIPS ';
  sponsors: Sponsor[] = [];
  //users: User[] = [];
  orgName: String = '';
  spoInfoForm: FormGroup = new FormGroup ({});
  displayedColumns: string[] = ['name', 'address', 'email', 'telephonenumber', 'moreInfoButton'];
  displayedColumnsReceived: string[] = ['date', 'subject', 'fromName', 'fromEmail'];
  displayedColumnsActiveProjects: string[] = ['title', 'creationDate', 'endofprojectDate', 'daysLeft'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  isLoading: boolean = false;
  displayEmail!: EmailDisplay;
  receivedEmails: Email[] = [];
  activeProjects: Project[] = [];
  currentDate: Date = new Date();
  
  @ViewChild(MatSort) sort: MatSort = new MatSort;  

  constructor(private apiService: ApiService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.getOrganizationReceivedEmails()
    this.getOrganizationInfo();
    this.getSponsorInfo();
    this.getActiveUserProjects();
  }

  updateTitle(value: string){
    this.title = value;
  }
  openModal() {
    //var dialogRef = this.dialog.open(AddUserModalComponent);

    // dialogRef.afterClosed().subscribe(() => this.getSponsorInfo());
  }

  getSponsorInfo() {
    this.apiService.getSponsors()
    .subscribe(sponsors => {
      if (sponsors.length !== 0) {
        this.sponsors = new Array(1);
      this.sponsors[0] = sponsors.reverse()[0];
      this.dataSource.data = this.sponsors;
      }
    })
  }

  getOrganizationReceivedEmails() {
    this.apiService.getOrganizationReceivedEmailList()
    .subscribe(emails => {
      if (emails.length !== 0) {
        this.receivedEmails = new Array(1)
      this.receivedEmails[0] = emails[0]
      }
    });
  }

  getOrganizationInfo() {
    this.apiService.getOrganization()
    .subscribe(org => {
      this.orgName = org.title ?? '';
    });
  }

  getActiveUserProjects() {
    this.apiService.getActiveUserProjects()
    .subscribe(x => {
      this.activeProjects = x;
    });
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

  getDaysDifference(endDate: Date): number {
    let a = new Date(endDate)
    const timeDiff = Math.abs(a.getTime() - this.currentDate.getTime());
    const diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays;
  }
}
