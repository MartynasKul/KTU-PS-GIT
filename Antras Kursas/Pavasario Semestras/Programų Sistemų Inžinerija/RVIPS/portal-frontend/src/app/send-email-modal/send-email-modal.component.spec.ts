import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendEmailModalComponent } from './send-email-modal.component';

describe('SendEmailModalComponent', () => {
  let component: SendEmailModalComponent;
  let fixture: ComponentFixture<SendEmailModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SendEmailModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SendEmailModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
