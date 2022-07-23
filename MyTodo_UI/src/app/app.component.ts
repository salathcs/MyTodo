import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [CookieService]
})
export class AppComponent {

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private router: Router,
    private cookieService: CookieService)
  { }

  public navigateToTodos(): void {
    const userId = this.cookieService.get("userId");

    this.router.navigate([`/todos/${userId}`]);
  }

  public logout(): void {
    this.cookieService.deleteAll();
    this.document.location.href = environment.authUIUrl;
  }

  title = 'My Todos app';
}
