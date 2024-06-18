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
import { AddSponsorModalComponent } from '../add-sponsor-modal/add-sponsor-modal.component';

@Component({
  selector: 'app-sponsor-page',
  templateUrl: './sponsor-page.component.html',
  styleUrls: ['./sponsor-page.component.scss']
})
export class SponsorPageComponent implements OnInit {
  sponsors: Sponsor[] = [];
  //users: User[] = [];
  orgName: String = '';
  spoInfoForm: FormGroup = new FormGroup ({});
  displayedColumns: string[] = ['name', 'address', 'email', 'telephonenumber', 'moreInfoButton'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  isLoading: boolean = false;
  active: boolean = false;

  @ViewChild(MatSort) sort: MatSort = new MatSort;

constructor(private apiService: ApiService, private dialog: MatDialog) {
}
  ngOnInit(): void {
    this.getOrganizationInfo();
    this.getSponsorInfo();
  }

  openModal() {
    var dialogRef = this.dialog.open(AddSponsorModalComponent);

    dialogRef.afterClosed().subscribe(() => this.getSponsorInfo());
  }

  getSponsorInfo() {
    this.apiService.getSponsors()
    .subscribe(sponsors => {
      this.sponsors = sponsors.reverse();
      this.dataSource.data = this.sponsors;
      this.active = true
    })
  }

  getOrganizationInfo() {
    this.apiService.getOrganization()
    .subscribe(org => {
      this.orgName = org.title ?? '';
    });
  }

}
