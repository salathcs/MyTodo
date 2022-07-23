import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TodosComponent } from './todos/todos.component';
import { UsersComponent } from './users/users.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSortModule } from '@angular/material/sort';
import { ErrorComponent } from './error/error.component';
import { MyRoutingComponent } from './my-routing/my-routing.component';
import { TodosGridComponent } from './todos/todos-grid/todos-grid.component';
import { TodosToolBarComponent } from './todos/todos-tool-bar/todos-tool-bar.component';
import { TodosModalComponent } from './todos/todos-modal/todos-modal.component';
import { UsersGridComponent } from './users/users-grid/users-grid.component';
import { UsersModalComponent } from './users/users-modal/users-modal.component';
import { UsersToolBarComponent } from './users/users-tool-bar/users-tool-bar.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    TodosComponent,
    UsersComponent,
    ErrorComponent,
    MyRoutingComponent,
    TodosGridComponent,
    TodosToolBarComponent,
    TodosModalComponent,
    UsersGridComponent,
    UsersModalComponent,
    UsersToolBarComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatInputModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatSlideToggleModule,
    MatSelectModule,
    MatOptionModule,
    MatTooltipModule,
    MatDialogModule,
    MatSortModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
