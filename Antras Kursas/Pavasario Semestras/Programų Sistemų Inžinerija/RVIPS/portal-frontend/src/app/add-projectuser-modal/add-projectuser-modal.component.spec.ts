import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectUserModalComponent } from './add-projectuser-modal.component';

describe('AddUserModalComponent', () => {
  let component: AddProjectUserModalComponent;
  let fixture: ComponentFixture<AddProjectUserModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddProjectUserModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddProjectUserModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
