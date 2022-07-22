import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [CookieService]
})
export class AppComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cookieService: CookieService)
  { }

  ngOnInit() {
    
  }

  public navigateToTodos(): void {
    const userId = this.cookieService.get("userId");

    this.router.navigate([`/todos/${userId}`]);
  }

  title = 'My Todos app';
}
