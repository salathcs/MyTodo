import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TodoDto } from '../../models/todo-dto';

@Component({
  selector: 'app-todos-modal',
  templateUrl: './todos-modal.component.html',
  styleUrls: ['./todos-modal.component.css']
})
export class TodosModalComponent {
  public title: string;
  public submitBtnTitle: string;

  public submitFailed: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<TodosModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TodoDto,
    private http: HttpClient,
    ) {
    this.title = data.id === 0 ? "Create Todo" : "Update Todo";
    this.submitBtnTitle = data.id === 0 ? "Create" : "Update";
  }

  onSubmit(): void {
    if (this.data.id > 0) {
      this.updateTodo();
    }
    else {
      this.createTodo();
    }
  }

  private createTodo(): void {
    this.http.post<TodoDto>('/api/todos/crud/', this.data).subscribe(result => {
      this.dialogRef.close(result);
    }, error => {
      console.error(error);
      this.submitFailed = true;
    });
  }

  private updateTodo(): void {
    this.http.put<TodoDto>('/api/todos/crud/', this.data).subscribe(_ => {
      this.dialogRef.close(this.data);
    }, error => {
      console.error(error);
      this.submitFailed = true;
    });
  }

}
