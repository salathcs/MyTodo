import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { AuthResult } from '../models/auth-result';
import { LoginService } from '../services/login-Service';

@Component({
  selector: 'app-login-sucess',
  templateUrl: './login-sucess.component.html',
  styleUrls: ['./login-sucess.component.css']
})
export class LoginSucessComponent implements OnInit {
  public countDown: number = 5;
  public countDownValid: boolean = false;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private loginService: LoginService  ) { }

  ngOnInit(): void {
    const authResult = this.loginService.getAuthResult();
    if (authResult !== undefined) {
      this.countDownValid = true;
      this.startCountDown(authResult);
    }
    else {
      this.countDownValid = false;
    }
  }

  private async startCountDown(authResult: AuthResult): Promise<void> {
    for (var i = 0; i < 5; i++) {
      await this.timeout(1000);
      --this.countDown;
    }
    
    this.document.location.href = authResult.redirectUrl;
  }

  private timeout(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

}
