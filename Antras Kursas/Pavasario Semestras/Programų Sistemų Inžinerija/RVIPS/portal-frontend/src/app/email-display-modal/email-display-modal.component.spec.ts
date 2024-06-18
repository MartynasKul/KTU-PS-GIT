import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailDisplayModalComponent } from './email-display-modal.component';

describe('EmailDisplayModalComponent', () => {
  let component: EmailDisplayModalComponent;
  let fixture: ComponentFixture<EmailDisplayModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailDisplayModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmailDisplayModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
