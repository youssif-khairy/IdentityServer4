import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { SigninCallbackComponent } from './components/signin-callback/signin-callback.component';



@NgModule({
  declarations: [
    NavBarComponent,
    HomeComponent,
    LoginComponent,
    SigninCallbackComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule,
    HttpClientModule
  ],
  exports :[
    NavBarComponent,
    HomeComponent
  ]
})
export class SharedModule { }
