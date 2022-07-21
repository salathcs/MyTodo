import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationSucessComponent } from './registration-sucess.component';

describe('RegistrationSucessComponent', () => {
  let component: RegistrationSucessComponent;
  let fixture: ComponentFixture<RegistrationSucessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistrationSucessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistrationSucessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
