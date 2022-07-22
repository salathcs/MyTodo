import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TodosComponent } from './todos/todos.component';
import { UsersComponent } from './users/users.component';
import { MyRoutingComponent } from './my-routing/my-routing.component';
import { ErrorComponent } from './error/error.component';


const routes: Routes = [
  { path: '', redirectTo: 'my-routing', pathMatch: 'prefix' },
  { path: 'my-routing', component: MyRoutingComponent },
  { path: 'error', component: ErrorComponent },
  { path: 'todos/:userId', component: TodosComponent },
  { path: 'users', component: UsersComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
})
export class AppRoutingModule { }
