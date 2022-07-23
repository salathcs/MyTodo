import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { TodoDto } from '../../models/todo-dto';

@Component({
  selector: 'app-todos-grid',
  templateUrl: './todos-grid.component.html',
  styleUrls: ['./todos-grid.component.css']
})
export class TodosGridComponent implements OnInit, AfterViewInit, OnDestroy {
  private userId?: string = undefined;

  public displayedColumns: string[] = ['title', 'description', 'expiration'];

  public selectedRow: TodoDto | null = null;

  @Input() onRefreshGrid!: Observable<TodoDto | null>;
  private onRefreshGridSubscription!: Subscription;

  @Output() onSelectedRowChanged: EventEmitter<TodoDto | null> = new EventEmitter();

  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params
      .subscribe(params => {
        this.userId = params["userId"];

        if (this.userId !== undefined) {
          this.getTodos();
        }
        else {
          this.router.navigate(['/error']);
        }
      }
    );

    if (this.onRefreshGrid !== undefined) {
      this.onRefreshGridSubscription = this.onRefreshGrid.subscribe(_ => this.getTodos());
    }
  }

  ngAfterViewInit() {
    if (this.sort !== undefined) {
      this.dataSource.sort = this.sort;
    }
  }

  ngOnDestroy(): void {
    this.onRefreshGridSubscription?.unsubscribe();
  }

  public selectRow(row: TodoDto) {
    if (this.selectedRow === row) {
      this.selectedRow = null;
    }
    else {
      this.selectedRow = row;
    }

    this.onSelectedRowChanged.emit(this.selectedRow);
  }

  private getTodos(): void {
    if (this.userId === undefined) {
      return;
    }

    this.http.get<TodoDto[]>(`/api/todos/manager/GetByUserId/${this.userId}`).subscribe(result => {
      this.dataSource.data = result;
      this.onSelectedRowChanged.emit(null);
    }, error => {
      console.error(error);
      this.router.navigate(['/error']);
    });
  }
}
