import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { UserDto } from '../../models/user-dto';

@Component({
  selector: 'app-users-grid',
  templateUrl: './users-grid.component.html',
  styleUrls: ['./users-grid.component.css']
})
export class UsersGridComponent implements OnInit, AfterViewInit, OnDestroy {

  public displayedColumns: string[] = ['name', 'email'];

  public selectedRow: UserDto | null = null;

  @Input() onRefreshGrid!: Observable<UserDto | null>;
  private onRefreshGridSubscription!: Subscription;

  @Output() onSelectedRowChanged: EventEmitter<UserDto | null> = new EventEmitter();

  public dataSource = new MatTableDataSource();
  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private http: HttpClient,
    private router: Router) { }

  ngOnInit(): void {
    this.getUsers();

    if (this.onRefreshGrid !== undefined) {
      this.onRefreshGridSubscription = this.onRefreshGrid.subscribe(_ => this.getUsers());
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

  public selectRow(row: UserDto) {
    if (this.selectedRow === row) {
      this.selectedRow = null;
    }
    else {
      this.selectedRow = row;
    }

    this.onSelectedRowChanged.emit(this.selectedRow);
  }

  private getUsers(): void {
    this.http.get<UserDto[]>('/api/users/crud').subscribe(result => {
      this.dataSource.data = result;
      this.onSelectedRowChanged.emit(null);
    }, error => {
      console.error(error);
      this.router.navigate(['/error']);
    });
  }

}
