import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
    private http: HttpClient,
    private cookieService: CookieService)
  { }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        console.log(params);
        const token = params['token'];
        this.cookieService.set('MyTodoToken', token);
      }
    );
  }

  public onSmthing(): void {
    this.http.get<any>('/api/users/').subscribe(result => {
      console.log("asd");
    }, error => console.error(error));
  }

  title = 'MyTodo_UI';
}
