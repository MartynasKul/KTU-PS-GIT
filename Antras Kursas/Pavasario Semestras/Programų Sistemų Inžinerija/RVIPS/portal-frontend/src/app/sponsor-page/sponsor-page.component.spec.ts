import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorPageComponent } from './sponsor-page.component';

describe('SponsorPageComponent', () => {
  let component: SponsorPageComponent;
  let fixture: ComponentFixture<SponsorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SponsorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
