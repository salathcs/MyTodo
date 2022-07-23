import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { TodoDto } from '../models/todo-dto';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  //from toolBar
  public refreshGrid: Subject<TodoDto | null> = new Subject<TodoDto | null>();

  //from grid
  public selectedRowChanged: Subject<TodoDto | null> = new Subject<TodoDto | null>();

  constructor() { }

  ngOnInit(): void { }

  //from toolBar
  public onRefreshGrid(event: TodoDto | null): void {
    this.refreshGrid.next(event);
  }

  //from grid
  public onSelectedRowChanged(event: TodoDto | null): void {
    this.selectedRowChanged.next(event);
  }

}
