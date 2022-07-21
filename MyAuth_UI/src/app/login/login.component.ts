import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthResult } from '../models/auth-result';
import { Router } from '@angular/router';
import { LoginService } from '../services/login-Service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginValid = true;
  public username = '';
  public password = '';

  constructor(    
    private http: HttpClient,
    private router: Router,
    private loginService: LoginService) { }

  public onSubmit(): void {
    this.http.post<AuthResult>('/auth/LogIn', { UserName: this.username, Password: this.password }).subscribe(result => {
      this.loginService.setAuthResult(result);
      this.router.navigate(['/login-success']);
    }, error => {
      console.error(error);
      this.loginValid = false;
    });
  }

}
