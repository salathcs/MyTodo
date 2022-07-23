import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserWithIdentityDto } from '../models/user-with-identity-dto';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  public name = '';
  public email = '';
  public username = '';
  public password = '';
  public passwordConfirmation = '';

  public passwordsDifferent: boolean = false;
  public registrationFailed: boolean = false;

  constructor(private http: HttpClient,
              private router: Router) { }

  public onSubmit(): void {
    if (this.valid()) {
      this.http.post<UserWithIdentityDto>('/auth/Register', { Id: 0, Name: this.name, Email: this.email, UserName: this.username, Password: this.password }).subscribe(_ => {
        this.router.navigate(['/registration-success']);
      }, error => {
        console.error(error);
        this.registrationFailed = true;
      });
    }
  }

  private valid(): boolean {
    this.passwordsDifferent = this.password !== this.passwordConfirmation;
    if (this.passwordsDifferent) {
      return false;
    }

    return true;
  }

}
