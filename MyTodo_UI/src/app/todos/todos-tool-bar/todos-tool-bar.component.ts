import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { TodoDto } from '../../models/todo-dto';
import { TodosModalComponent } from '../todos-modal/todos-modal.component';

@Component({
  selector: 'app-todos-tool-bar',
  templateUrl: './todos-tool-bar.component.html',
  styleUrls: ['./todos-tool-bar.component.css']
})
export class TodosToolBarComponent implements OnInit, OnDestroy {
  public title = "Todos grid";

  public selectedTodo: TodoDto | null = null;

  @Input() onSelectedRowChanged!: Observable<TodoDto | null>;
  private onSelectedRowChangedSubscription!: Subscription;

  @Output() onRefresh: EventEmitter<TodoDto | null> = new EventEmitter();

  constructor(
    public dialog: MatDialog,
    private http: HttpClient) { }

  ngOnInit(): void {
    if (this.onSelectedRowChanged !== undefined) {
      this.onSelectedRowChangedSubscription = this.onSelectedRowChanged.subscribe(todoDto => this.selectedTodo = todoDto);
    }
  }

  ngOnDestroy(): void {
    this.onSelectedRowChangedSubscription?.unsubscribe();
  }

  public openCreateDialog(): void {
    const dialogRef = this.dialog.open(TodosModalComponent, {
      width: '450px',
      data: { id: 0, title: "", description: "", expiration: null, userId: 0 }
    });

    dialogRef.afterClosed().subscribe(this.afterClosedGridUpdate);
  }

  public openUpdateDialog(): void {
    if (this.selectedTodo === null) {
      return;
    }

    const dialogRef = this.dialog.open(TodosModalComponent, {
      width: '450px',
      data: Object.assign({}, this.selectedTodo)
    });

    dialogRef.afterClosed().subscribe(this.afterClosedGridUpdate);
  }

  public deleteSelected(): void {
    if (this.selectedTodo === null) {
      return;
    }

    if (confirm("Are you sure to delete the selected item?")) {
      this.http.delete<TodoDto>(`/api/todos/crud/${this.selectedTodo.id}`).subscribe(_ => {
        this.refreshGrid(null);
      }, error => {
        console.error(error);
        alert("Delete failed!");
      });
    }
  }

  public refreshGrid(todoDto: TodoDto | null): void {
    this.onRefresh.emit(todoDto);
  }

  private afterClosedGridUpdate = (result: any) => {
    const todoDtoResult = result as TodoDto;

    if (todoDtoResult !== undefined) {
      this.refreshGrid(todoDtoResult);
    }
  };

}
