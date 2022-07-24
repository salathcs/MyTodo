import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserDto } from '../../models/user-dto';

@Component({
  selector: 'app-users-modal',
  templateUrl: './users-modal.component.html',
  styleUrls: ['./users-modal.component.css']
})
export class UsersModalComponent implements OnInit {
  public title: string;
  public submitBtnTitle: string;

  public submitFailed: boolean = false;

  public adminRight: boolean = false;

  public adminRightChecked: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<UsersModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDto,
    private http: HttpClient,
  ) {
    this.title = data.id === 0 ? "Create user" : "Update user";
    this.submitBtnTitle = data.id === 0 ? "Create" : "Update";
  }

  ngOnInit(): void {
    //authenticated user is admin?
    this.http.head<any>('/api/users/manager/HasAdminRight').subscribe(_ => {
      this.adminRight = true;
      //if admin, then the selected user is admin?
      this.http.head<any>(`/api/users/manager/DoesUserHaveAdminRight/${this.data.id}`).subscribe(_ => {
        this.adminRightChecked = true;
      }, _ => {
        this.adminRightChecked = false;
      });
    }, _ => {
      this.adminRight = false;
    });
  }

  onSubmit(): void {
    if (this.data.id > 0) {
      this.updateUserSelector();
    }
    else {
      this.createUser();
    }
  }

  private createUser(): void {
    this.http.post<UserDto>('/api/users/crud/', this.data).subscribe(result => {
      this.dialogRef.close(result);
    }, error => {
      console.error(error);
      this.submitFailed = true;
    });
  }

  private updateUserSelector(): void {
    if (!this.adminRight) {
      this.updateUser();
    }
    else if (this.adminRightChecked) {
      this.updateUserAndAddAdminRight();
    }
    else {
      this.updateUserAndRemoveAdminRight();
    }
  }

  private updateUser(): void {
    this.http.put<UserDto>('/api/users/crud/', this.data).subscribe(_ => {
      this.dialogRef.close(this.data);
    }, error => {
      console.error(error);
      this.submitFailed = true;
    });
  }

  private updateUserAndAddAdminRight(): void {
    this.http.put<UserDto>('/api/users/manager/UpdateUserAndAddAdminRight', this.data).subscribe(_ => {
      this.dialogRef.close(this.data);
    }, error => {
      console.error(error);
      this.submitFailed = true;
    });
  }

  private updateUserAndRemoveAdminRight(): void {
    this.http.put<UserDto>('/api/users/manager/UpdateUserAndRemoveAdminRight', this.data).subscribe(_ => {
      this.dialogRef.close(this.data);
    }, error => {
      console.error(error);
      this.submitFailed = true;
    });
  }

}
