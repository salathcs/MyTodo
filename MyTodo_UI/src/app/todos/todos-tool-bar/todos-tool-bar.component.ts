import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TodosModalComponent } from '../todos-modal/todos-modal.component';

@Component({
  selector: 'app-todos-tool-bar',
  templateUrl: './todos-tool-bar.component.html',
  styleUrls: ['./todos-tool-bar.component.css']
})
export class TodosToolBarComponent implements OnInit {
  public title = "Todos grid";

  constructor(
    public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  public openCreateDialog(): void {
    const dialogRef = this.dialog.open(TodosModalComponent, {
      width: '450px',
      data: { id: 0, title: "", description: "", expiration: null, userId: 0 }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //TODO create
    });
  }

  public tmp(): void {
    console.log("a");
  }

}
