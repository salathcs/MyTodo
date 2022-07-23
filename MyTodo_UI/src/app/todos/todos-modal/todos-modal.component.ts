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
    ) {
    this.title = data.id === 0 ? "Create Todo" : "Update Todo";
    this.submitBtnTitle = data.id === 0 ? "Create" : "Update";
  }

  onSubmit(): void {
    this.dialogRef.close(this.data);
  }

}
