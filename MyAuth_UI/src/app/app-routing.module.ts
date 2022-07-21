import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginSucessComponent } from './login-sucess/login-sucess.component';
import { RegistrationSucessComponent } from './registration-sucess/registration-sucess.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'prefix' },
  { path: 'login', component: LoginComponent },
  { path: 'login-success', component: LoginSucessComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'registration-success', component: RegistrationSucessComponent },
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
