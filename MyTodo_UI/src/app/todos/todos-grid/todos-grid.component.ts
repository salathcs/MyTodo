import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TodoDto } from '../../models/todo-dto';

@Component({
  selector: 'app-todos-grid',
  templateUrl: './todos-grid.component.html',
  styleUrls: ['./todos-grid.component.css']
})
export class TodosGridComponent implements OnInit {
  public todoList: TodoDto[] = [];
  public displayedColumns: string[] = ['title', 'description', 'expiration'];

  public selectedRow?: TodoDto;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params
      .subscribe(params => {
        const userId = params["userId"];

        if (userId !== undefined) {
          this.getTodos();
        }
        else {
          this.router.navigate(['/error']);
        }
      }
      );
  }

  private getTodos(): void {
    this.http.get<TodoDto[]>('/api/todos/').subscribe(result => {
      this.todoList = result;
    }, error => {
      console.error(error);
      this.router.navigate(['/error']);
    });
  }
}
