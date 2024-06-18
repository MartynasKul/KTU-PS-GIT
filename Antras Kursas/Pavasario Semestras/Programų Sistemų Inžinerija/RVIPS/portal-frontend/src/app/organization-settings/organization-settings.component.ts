import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { ApiService } from '../api-service.service';
import { MatTableDataSource } from '@angular/material/table';
import {User} from '../models/user.model'
import { MatDialog } from '@angular/material/dialog';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { Organization } from '../models/organization.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-organization-settings',
  templateUrl: './organization-settings.component.html',
  styleUrls: ['./organization-settings.component.scss']
})
export class OrganizationSettingsComponent implements OnInit {
  users: User[] = [];
  orgName: String = '';

  imapServerName: String = '';
  imapServerPort: String = '';
  imapServeUserName: String = '';
  imapServerPassword: String = '';

  smtpServerName: String = '';
  smtpServerPort: String = '';
  smtpServeUserName: String = '';
  smtpServerPassword: String = '';

  orgRenameForm: FormGroup = new FormGroup({});
  imapServerForm: FormGroup = new FormGroup({});
  smtpServerForm: FormGroup = new FormGroup({});

  displayedColumns: string[] = ['name', 'lastName', 'email', 'creationDate', 'roles'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  isLoading: boolean = false;
  isLoadingImap: boolean = false;
  isLoadingSmtp: boolean = false;

  unableImap: boolean = false;
  unableSmtp: boolean = false;

  doneImap: boolean = false;
  doneSmtp: boolean = false;

  @ViewChild(MatSort) sort: MatSort = new MatSort;

constructor(private apiService: ApiService, private dialog: MatDialog) {
  this.orgRenameForm = new FormGroup({
    name: new FormControl('', [Validators.required])
  });

  this.imapServerForm = new FormGroup({
    imapServerName: new FormControl('', [Validators.required]),
    imapServerPort: new FormControl('', [Validators.required]),
    imapServeUserName: new FormControl('', [Validators.required]),
    imapServerPassword: new FormControl('', [Validators.required])
  });

  this.smtpServerForm = new FormGroup({
    smtpServerName: new FormControl('', [Validators.required]),
    smtpServerPort: new FormControl('', [Validators.required]),
    smtpServeUserName: new FormControl('', [Validators.required]),
    smtpServerPassword: new FormControl('', [Validators.required])
  });
}
  ngOnInit(): void {
    this.getOrganizationUsers();
    this.getOrganizationInfo();
  }

  openModal() {
    var dialogRef = this.dialog.open(AddUserModalComponent);

    dialogRef.afterClosed().subscribe(() => this.getOrganizationUsers());
  }
  

  getOrganizationUsers() {
    this.apiService.getOrganizationUserList()
    .subscribe(users => {
      this.users = users;
      this.dataSource.data = this.users;
    });
  }

  getOrganizationInfo() {
    this.apiService.getOrganization()
    .subscribe(org => {
      this.orgName = org.title ?? '';

      this.imapServerName = org.imapServer ?? '';
      this.imapServerPort = org.imapServerPort?.toString() ?? '';
      this.imapServeUserName = org.imapServerUserName ?? "";

      this.smtpServerName = org.smtpServer ?? '';
      this.smtpServerPort = org.smtpServerPort?.toString() ?? '';
      this.smtpServeUserName = org.smtpServerUserName ?? "";
    });
  }

  renameOrg() {
    if (this.orgRenameForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.renameOrganization(
      this.orgRenameForm.value.name)
      .subscribe(() => {
        this.getOrganizationInfo();
        this.isLoading = false;
      }, error => {
        this.isLoading = false;
        console.error(error);
      });
  }

  addImapServer() {
    if (this.imapServerForm.invalid) {
      return;
    }
    this.unableImap = false;
    this.isLoadingImap = true;
    this.doneImap = false;
    this.apiService.addOrganizationImapServer(
      this.imapServerForm.value.imapServerName,
      Number(this.imapServerForm.value.imapServerPort),
      this.imapServerForm.value.imapServeUserName,
      this.imapServerForm.value.imapServerPassword)
      .subscribe(() => {
        this.getOrganizationInfo();
        this.isLoadingImap = false;
        this.doneImap = true;
      }, error => {
        this.isLoadingImap = false;
        this.unableImap = true;
        console.error(error);
      })
  }

  addSmtpServer() {
    if (this.smtpServerForm.invalid) {
      return;
    }
    this.unableSmtp = false;
    this.isLoadingSmtp = true;
    this.doneSmtp = false;
    this.apiService.addOrganizationSmtpServer(
      this.smtpServerForm.value.smtpServerName,
      Number(this.smtpServerForm.value.smtpServerPort),
      this.smtpServerForm.value.smtpServeUserName,
      this.smtpServerForm.value.smtpServerPassword)
      .subscribe(() => {
        this.getOrganizationInfo();
        this.isLoadingSmtp = false;
        this.doneSmtp = true;
      }, error => {
        this.isLoadingSmtp = false;
        this.unableSmtp = true;
        console.error(error);
      })
  }
}
