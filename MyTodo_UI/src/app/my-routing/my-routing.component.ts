import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-my-routing',
  templateUrl: './my-routing.component.html',
  styleUrls: ['./my-routing.component.css']
})
export class MyRoutingComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cookieService: CookieService  ) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
        const name = params['name'];
        const userId = params['userId'];
        const expirationTicks = Number.parseInt(params['expiration']);
        const expiration = new Date(expirationTicks); 
        const token = params['token'];
        this.cookieService.set('name', name, expiration);
        this.cookieService.set('userId', userId, expiration);
        this.cookieService.set('MyTodoToken', token, expiration);

        if (token !== undefined) {
          this.router.navigate([`/todos/${userId}`]);
        }
        else {
          this.router.navigate(['/error']);
        }
      }
    );
  }

  private getDateFromTicks(ticks: string): Date | undefined {
    const numTicks = Number.parseInt(ticks);
    if (numTicks > 0) {
      return new Date(numTicks / 1e+4 + new Date('0001-01-01T00:00:00Z').getTime());
    }

    return undefined;
  }

}
