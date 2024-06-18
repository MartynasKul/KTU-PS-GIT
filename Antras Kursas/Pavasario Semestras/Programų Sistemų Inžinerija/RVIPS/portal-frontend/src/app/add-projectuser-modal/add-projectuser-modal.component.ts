import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from '../api-service.service';
import { Router } from '@angular/router';
import { User } from '../models/user.model'
import { MatTableDataSource } from '@angular/material/table';
import { Project } from '../models/project.model';

@Component({
  selector: 'app-add-projectuser-modal',
  templateUrl: './add-projectuser-modal.component.html',
  styleUrls: ['./add-projectuser-modal.component.scss']
})
export class AddProjectUserModalComponent {
  registrationForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isDuplicate = false;
  notMatchingPassword = false;

  users: User[] = [];
  selectuserVal: string = '';
  project: Project = {
    id: -1,
    title: '',
    description:'',
    creationDate:new Date(),
    endofprojectDate: new Date(),
    users: new Array(),
    sponsors: new Array()
  }

  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  constructor(
    private dialogRef: MatDialogRef<AddProjectUserModalComponent>,
    @Inject(MAT_DIALOG_DATA) public project_inj: Project,
    private apiService: ApiService,
    private router: Router) {
    this.project = project_inj
    this.registrationForm = new FormGroup({
      userToProjectId:  new FormControl('0', [Validators.required])
    });
  }
  ngOnInit(): void {
    this.getOrganizationUsers();
  }

  getOrganizationUsers() {
    this.apiService.getOrganizationUserList()
    .subscribe(users => {
      this.users = users;


      for (let user of this.project.users) {
        this.users = this.users.filter(x => x.id !== user.id)
        this.dataSource.data = this.users;
      }
      
    });
  }

  submitForm() {
    var projectval = Number(this.selectuserVal);
    this.registrationForm.value.userToProjectId = projectval;
    if(this.selectuserVal == '')
    {
      this.isDuplicate = true;
      return;
    }

    if (this.registrationForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.addUserToProject(
      this.registrationForm.value.userToProjectId,
      this.project.id)
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
