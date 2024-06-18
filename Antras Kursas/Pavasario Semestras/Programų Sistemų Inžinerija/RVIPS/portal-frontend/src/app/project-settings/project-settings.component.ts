import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { ApiService } from '../api-service.service';
import { MatTableDataSource } from '@angular/material/table';
import { User } from '../models/user.model'
import { MatDialog } from '@angular/material/dialog';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { Organization } from '../models/organization.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Sponsor } from '../models/sponsor.model'
import { AddProjectModalComponent } from '../add-project-modal/add-project-modal.component';
import { Project } from '../models/project.model';
import { AddProjectUserModalComponent } from '../add-projectuser-modal/add-projectuser-modal.component';
import { AddSponsorModalComponent } from '../add-sponsor-modal/add-sponsor-modal.component';
import { AddProjectSponsorModalComponent } from '../add-project-sponsor-modal/add-project-sponsor-modal.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-project-settings',
  templateUrl: './project-settings.component.html',
  styleUrls: ['./project-settings.component.scss']
})
export class ProjectSettingsComponent implements OnInit {
  projectName: String = '';
  sponsors: Sponsor[] = [];
  users: User[] = [];
  //projects = ['projektas1', 'projektas2', 'projektas3', 'projektas4'];
  projects: Project[] = [];
  selectVal: number = 0;
  proName: String = '';
  proInfoForm: FormGroup = new FormGroup({});
  NewProject: Project[] = [];
  displayedsponsorColumns: string[] = ['name', 'address', 'email', 'telephonenumber', 'moreInfoButton'];
  displayeduserColumns: string[] = ['name', 'lastName', 'email'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  isLoading: boolean = false;
  showAllowed: boolean = false;
  isDuplicate = false;
  currentProject!: Project | undefined;
  doneUpdate: boolean = false;

  @ViewChild(MatSort) sort: MatSort = new MatSort;

constructor(private apiService: ApiService, private dialog: MatDialog, private cdr: ChangeDetectorRef, private datePipe: DatePipe) {
  this.proInfoForm = new FormGroup({
      name:  new FormControl('', [Validators.required]),
      description:  new FormControl('', [Validators.required]),
      creationDate: new FormControl('', [Validators.required]),
      endOfProjectDate: new FormControl('', [Validators.required]),
  });

  this.proInfoForm.get('name')?.valueChanges.subscribe((value: string) => {
    this.getUsersFromProject()
    this.getSponsorsFromProject()
  });
}
  ngOnInit(): void {
    this.getProjectInfo();
  }

  onDropdownSelection(): void {
    setTimeout(() => {
      this.getProjectInfo()
      this.doneUpdate = false
      this.cdr.detectChanges();
    });
  }
  getUsersFromProject(){
    for(let project of this.projects){
      if(project.id == this.selectVal){
        this.users = project.users;

        this.users = this.users.map(user => {
          const displayUser: User = {
            id: user.id,
            name: user.name,
            lastName: user.lastName,
            email: user.email,
            creationDate: new Date(user.creationDate),
            roles: user.roles,
            organizationId: user.organizationId
          };
          return displayUser;
        });

        return;
      }
    }
  }

  getSponsorsFromProject(){
    for(let project of this.projects){
      if(project.id == this.selectVal){
        this.sponsors = project.sponsors;

        this.sponsors = this.sponsors.map(sponsor => {
          const displaySponsor: Sponsor = {
            id: sponsor.id,
            sponsorName: sponsor.sponsorName,
            telephoneNumber: sponsor.telephoneNumber,
            address: sponsor.address,
            email: sponsor.email,
            description: sponsor.description
          };
          return displaySponsor;
        });

        return;
      }
    }
  }

  getCurrentProject() {
    for(let project of this.projects){
      
      if(project.id === this.selectVal){
      
        return project;
      }
    }

    return 
  }


  openProjectModal() {
    this.doneUpdate = false
    this.isDuplicate = false
    var dialogRef = this.dialog.open(AddProjectModalComponent);

    dialogRef.afterClosed().subscribe(() => this.getProjectInfo());
  }

  openUserModal() {
    this.doneUpdate = false
    this.isDuplicate = false
    var dialogRef = this.dialog.open(AddProjectUserModalComponent, {
      data: this.getCurrentProject()
    });

    dialogRef.afterClosed().subscribe(() => this.getProjectInfo());
  }

  openSponsorModal() {
    this.doneUpdate = false
    this.isDuplicate = false
    var dialogRef = this.dialog.open(AddProjectSponsorModalComponent, {
      data: this.getCurrentProject()
    });

    dialogRef.afterClosed().subscribe(() => this.getProjectInfo());
  }
  
  getProjectInfo() {
    this.apiService.getProjects()
    .subscribe(projects => {
      this.projects = projects;
      this.getUsersFromProject();
      this.getSponsorsFromProject();
      this.currentProject = this.getCurrentProject();
      this.proInfoForm = new FormGroup({
        name:  new FormControl(this.currentProject?.title, [Validators.required]),
        description:  new FormControl(this.currentProject?.description, [Validators.required]),
        creationDate: new FormControl(this.getFormattedDate(this.currentProject?.creationDate), [Validators.required]),
        endOfProjectDate: new FormControl(this.getFormattedDate(this.currentProject?.endofprojectDate), [Validators.required]),
    });
    });
  }

  getFormattedDate(date: Date | undefined): string {
    if (date) {
      return this.datePipe.transform(date, 'yyyy-MM-dd') || '';
    }
    return '';
  }

  submitForm() {
    this.doneUpdate = false
    this.isDuplicate = false
    if (this.proInfoForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.apiService.updateProject(
      this.currentProject?.id ?? 0,
      this.proInfoForm.value.name,
      this.proInfoForm.value.description,
      this.proInfoForm.value.creationDate,
      this.proInfoForm.value.endOfProjectDate)
      .subscribe(() => {
        this.getProjectInfo();
        this.isLoading = false;
        this.doneUpdate = true;
      }, error => {
        this.isLoading = false;
        this.isDuplicate = true
        console.error(error);
      });
  }
}
