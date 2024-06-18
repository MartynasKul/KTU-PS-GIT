import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectSponsorModalComponent } from './add-project-sponsor-modal.component';

describe('AddProjectSponsorModalComponent', () => {
  let component: AddProjectSponsorModalComponent;
  let fixture: ComponentFixture<AddProjectSponsorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddProjectSponsorModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddProjectSponsorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
