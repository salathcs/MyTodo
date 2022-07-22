import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRoutingComponent } from './my-routing.component';

describe('MyRoutingComponent', () => {
  let component: MyRoutingComponent;
  let fixture: ComponentFixture<MyRoutingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyRoutingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyRoutingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
