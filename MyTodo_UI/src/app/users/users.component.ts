import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { UserDto } from '../models/user-dto';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {

  //from toolBar
  public refreshGrid: Subject<UserDto | null> = new Subject<UserDto | null>();

  //from grid
  public selectedRowChanged: Subject<UserDto | null> = new Subject<UserDto | null>();

  constructor() { }

  //from toolBar
  public onRefreshGrid(event: UserDto | null): void {
    this.refreshGrid.next(event);
  }

  //from grid
  public onSelectedRowChanged(event: UserDto | null): void {
    this.selectedRowChanged.next(event);
  }

}
