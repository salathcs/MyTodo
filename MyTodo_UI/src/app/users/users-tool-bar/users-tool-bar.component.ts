import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { UserDto } from '../../models/user-dto';
import { UsersModalComponent } from '../users-modal/users-modal.component';

@Component({
  selector: 'app-users-tool-bar',
  templateUrl: './users-tool-bar.component.html',
  styleUrls: ['./users-tool-bar.component.css']
})
export class UsersToolBarComponent implements OnInit, OnDestroy {
  public title = "Todos grid";

  public selectedUser: UserDto | null = null;

  @Input() onSelectedRowChanged!: Observable<UserDto | null>;
  private onSelectedRowChangedSubscription!: Subscription;

  @Output() onRefresh: EventEmitter<UserDto | null> = new EventEmitter();

  constructor(
    public dialog: MatDialog,
    private http: HttpClient,
    private router: Router) { }

  ngOnInit(): void {
    if (this.onSelectedRowChanged !== undefined) {
      this.onSelectedRowChangedSubscription = this.onSelectedRowChanged.subscribe(userDto => this.selectedUser = userDto);
    }
  }

  ngOnDestroy(): void {
    this.onSelectedRowChangedSubscription?.unsubscribe();
  }

  public openCreateDialog(): void {
    const dialogRef = this.dialog.open(UsersModalComponent, {
      width: '450px',
      data: { id: 0, title: "", description: "", expiration: null, userId: 0 }
    });

    dialogRef.afterClosed().subscribe(this.afterClosedGridUpdate);
  }

  public openUpdateDialog(): void {
    if (this.selectedUser === null) {
      return;
    }

    const dialogRef = this.dialog.open(UsersModalComponent, {
      width: '450px',
      data: Object.assign({}, this.selectedUser)
    });

    dialogRef.afterClosed().subscribe(this.afterClosedGridUpdate);
  }

  public deleteSelected(): void {
    if (this.selectedUser === null) {
      return;
    }

    if (confirm("Are you sure to delete the selected item?")) {
      this.http.delete<UserDto>(`/api/users/crud/${this.selectedUser.id}`).subscribe(_ => {
        this.refreshGrid(null);
      }, error => {
        console.error(error);
        alert("Delete failed!");
      });
    }
  }

  public refreshGrid(userDto: UserDto | null): void {
    this.onRefresh.emit(userDto);
  }

  public navigateToUserTodos(): void {
    if (this.selectedUser === null) {
      return;
    }

    this.router.navigate([`/todos/${this.selectedUser.id}`]);
  }

  private afterClosedGridUpdate = (result: any) => {
    const userDtoResult = result as UserDto;

    if (userDtoResult !== undefined) {
      this.refreshGrid(userDtoResult);
    }
  };
}
