import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router, UrlSerializer } from '@angular/router';
import { environment } from '../../environments/environment.prod';
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

  private authResult?: AuthResult;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private loginService: LoginService,
    private router: Router,
    private serializer: UrlSerializer,
  ) { }

  ngOnInit(): void {
    const authResult = this.loginService.getAuthResult();
    if (authResult !== undefined) {
      this.countDownValid = true;
      this.authResult = authResult;
      this.startCountDown();
    }
    else {
      this.countDownValid = false;
    }
  }

  public redirectImmediately(): void {
    this.document.location.href = this.createRedirectUrl();
  }

  private async startCountDown(): Promise<void> {
    for (; this.countDown > 0; this.countDown--) {
      await this.timeout(1000);
    }
    
    this.document.location.href = this.createRedirectUrl();
  }

  private createRedirectUrl(): string {
    if (this.authResult === undefined) {
      return "";
    }

    const expirationDate = new Date(this.authResult.expiration ?? 0);

    const tree = this.router.createUrlTree(['/'], {
      queryParams: {
        name: this.authResult.name,
        userId: this.authResult.userId,
        expiration: expirationDate.getTime(),
        token: this.authResult.token
      }
    });
    const urlQueryPart = this.serializer.serialize(tree)

    return `${environment.appUIUrl}${urlQueryPart}`;
  }

  private timeout(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

}
