import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodosGridComponent } from './todos-grid.component';

describe('TodosGridComponent', () => {
  let component: TodosGridComponent;
  let fixture: ComponentFixture<TodosGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TodosGridComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TodosGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
