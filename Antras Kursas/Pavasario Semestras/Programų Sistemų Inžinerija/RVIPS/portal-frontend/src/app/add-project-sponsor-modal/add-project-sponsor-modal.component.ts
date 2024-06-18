import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ApiService } from '../api-service.service';
import { Project } from '../models/project.model';
import { Sponsor } from '../models/sponsor.model';

@Component({
  selector: 'app-add-project-sponsor-modal',
  templateUrl: './add-project-sponsor-modal.component.html',
  styleUrls: ['./add-project-sponsor-modal.component.scss']
})
export class AddProjectSponsorModalComponent {
  registrationForm: FormGroup = new FormGroup({});
  isLoading: Boolean = false;
  isDuplicate = false;
  notMatchingPassword = false;

  sponsors: Sponsor[] = [];
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
    private dialogRef: MatDialogRef<AddProjectSponsorModalComponent>,
    @Inject(MAT_DIALOG_DATA) public project_inj: Project,
    private apiService: ApiService,
    private router: Router) {
    this.project = project_inj
    this.registrationForm = new FormGroup({
      sponsorToProjectId:  new FormControl('0', [Validators.required])
    });
  }
  ngOnInit(): void {
    this.getOrganizationSponsors();
  }

  getOrganizationSponsors() {
    this.apiService.getSponsors()
    .subscribe(sponsors => {
      this.sponsors = sponsors;

      for (let sponsor of this.project.sponsors) {
        this.sponsors = this.sponsors.filter(x => x.id !== sponsor.id)
        this.dataSource.data = this.sponsors;
      }
      
    });
  }

  submitForm() {
    var projectval = Number(this.selectuserVal);
    this.registrationForm.value.sponsorToProjectId = projectval;
    if(this.selectuserVal == '')
    {
      this.isDuplicate = true;
      return;
    }

    if (this.registrationForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.apiService.addSponsorToProject(
      this.registrationForm.value.sponsorToProjectId,
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
