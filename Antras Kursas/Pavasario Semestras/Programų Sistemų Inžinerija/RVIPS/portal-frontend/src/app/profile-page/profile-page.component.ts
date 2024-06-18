import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { ApiService } from '../api-service.service';
import { MatTableDataSource } from '@angular/material/table';
import {User, UserType, Role} from '../models/user.model'
import { MatDialog } from '@angular/material/dialog';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { Organization } from '../models/organization.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  users: User[] = [];
  userName: String = '';
  userEmail: String = '';
  userRoles: Role[] = []; 
  userRole: string = '';
  userDa: Date = new Date();
  userDate: string =  '';
  orgName: String = '';
  WasChanged: Boolean = false;
  
  changePasswordForm: FormGroup = new FormGroup({});
  isLoading: boolean = false;
  passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  notMatchingPassword = false;

  @ViewChild(MatSort) sort: MatSort = new MatSort;

  constructor(private apiService: ApiService, private dialog: MatDialog) {
    this.changePasswordForm = new FormGroup({
      password: new FormControl('', [
        Validators.pattern(this.passwordRegex)]),
      repeatedPassword: new FormControl('', [Validators.required, Validators.minLength(8)])
    })
  }
    ngOnInit(): void {

      this.getOrganizationInfo();
      this.getUserData();
    }
  
    getUserData(){
        this.apiService.getUser().subscribe(User => {
        this.userName = User.name ?? '';
        this.userEmail = User.email ?? '';
        this.userRoles = User.roles ?? '';
        this.userDa = User.creationDate ?? '';
        this.userRole = this.userRoles.toString();
        this.userDate = this.userDa.toDateString();
      });
    }

    getOrganizationInfo() {
      this.apiService.getOrganization()
      .subscribe(org => {
        this.orgName = org.title ?? '';
      });
    }
  

    changeUserPassword() {
      this.notMatchingPassword = false;
      this.WasChanged = false;
      if (this.changePasswordForm.value.password != this.changePasswordForm.value.repeatedPassword) {
        this.notMatchingPassword = true;
        return;
      }
      if (this.changePasswordForm.invalid) {
        return;
      }
        this.isLoading = true;
       
        this.apiService.changePassword(
          this.changePasswordForm .value.password)
          .subscribe(() => {
            this.getUserData();
            this.isLoading = false;
            this.WasChanged = true;
          }, error => {
            this.isLoading = false;
            console.error(error);
          });
    }

}
