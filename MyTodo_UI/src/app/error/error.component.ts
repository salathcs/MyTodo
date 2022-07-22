import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent {

  constructor(@Inject(DOCUMENT) private document: Document) { }

  public redirectToLogin(): void {
    this.document.location.href = environment.authUIUrl;
  }

}
