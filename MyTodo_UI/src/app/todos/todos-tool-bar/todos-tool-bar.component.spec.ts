import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodosToolBarComponent } from './todos-tool-bar.component';

describe('TodosToolBarComponent', () => {
  let component: TodosToolBarComponent;
  let fixture: ComponentFixture<TodosToolBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TodosToolBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TodosToolBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
