import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersToolBarComponent } from './users-tool-bar.component';

describe('UsersToolBarComponent', () => {
  let component: UsersToolBarComponent;
  let fixture: ComponentFixture<UsersToolBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersToolBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersToolBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
