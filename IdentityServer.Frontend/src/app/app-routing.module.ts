import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './shared/components/login/login.component';
import { HomeComponent } from './shared/components/home/home.component';
import { SigninCallbackComponent } from './shared/components/signin-callback/signin-callback.component';
import { SignoutCallbackComponent } from './shared/components/signout-callback/signout-callback.component';

const routes: Routes = [
  {
    path : "", component : HomeComponent ,
    children :[
      
    ]
  },
  {
    path : "signin-callback", component : SigninCallbackComponent
  },
  {
    path : "signout-callback", component : SignoutCallbackComponent
  },
  {
    path : "login", component : LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
